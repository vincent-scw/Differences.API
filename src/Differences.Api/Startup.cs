using System;
using System.Collections.Generic;
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
using Differences.Domain.Articles;

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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = string.Format("https://login.microsoftonline.com/tfp/{0}/{1}/v2.0/",
                    Configuration["Authentication:AzureAdB2C:Tenant"], Configuration["Authentication:AzureAdB2C:Policy"]);
                o.Audience = Configuration["Authentication:AzureAdB2C:ClientId"];

                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailed
                };
             });

            services.AddDbContext<DifferencesDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Differences")));

            // Add framework services.
            services.AddMvc().AddJsonOptions(options =>
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

            app.UseStaticFiles();
            app.UseMvc();
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
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
        }

        private static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void InjectOthers(IServiceCollection services)
        {
            services.AddScoped<DifferencesDbContext>();
        }
    }
}
