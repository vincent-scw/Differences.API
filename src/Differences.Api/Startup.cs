using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Differences.Interaction.Repositories;
using Differences.DataAccess.Repositories;
using Differences.DataAccess;
using Differences.Domain.Questions;
using Differences.Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Differences.Common.Configuration;
using Differences.Domain;
using Differences.Domain.LikeRecords;
using Differences.OAuth2Provider;
using Differences.OAuth2Provider.Configuration;
using Microsoft.IdentityModel.Tokens;
using Differences.Api.Authentication;

namespace Differences.Api
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InjectRepositories(services);
            InjectServices(services);
            InjectGraphQL(services);
            InjectOthers(services);

            // Add service and create Policy with options 
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            AddAuthentication(services);

            services.Configure<OpenIdAuthorization>(Configuration.GetSection("OpenIdAuthorization"));
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddDbContext<DifferencesDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Differences")));

            services.AddLocalization();

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // use standard name conversion of properties
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();

                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }

            // global policy, if assigned here (it could be defined indvidually for each controller) 
            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings());

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !System.IO.Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            var supportedCultures = new[] {new CultureInfo("zh-CN")};

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(new CultureInfo("zh-CN")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("JwtConfig");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],

                // Validate the token expiry
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
#if DEBUG
                    // don't use https for debug
                    o.RequireHttpsMetadata = false;
#endif
                    o.TokenValidationParameters = tokenValidationParameters;
                });
        }

        private Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
#if DEBUG
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            arg.Response.ContentLength = s.Length;
            arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
#endif
            return Task.FromResult(0);
        }

        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ILikeRecordRepository, LikeRecordRepository>();
        }

        private static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IUserContextService, HttpUserContextService>();

            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILikeRecordService, LikeRecordService>();
        }

        private static void InjectOthers(IServiceCollection services)
        {
            services.AddScoped<DifferencesDbContext>();
            services.AddSingleton<OAuth2ProviderFactory>();
        }
    }
}
