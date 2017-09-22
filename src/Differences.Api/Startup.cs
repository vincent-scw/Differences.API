using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //});

            services.AddAuthorization();
            //    (options =>
            //{
            //    // Policy for dashboard: only administrator role.
            //    options.AddPolicy(Policies.AdministratorControl, policy => policy.RequireRole("administrator"));
            //    // Policy for resources: user or administrator roles. 
            //    options.AddPolicy(Policies.AccessResourcesControl, policy => policy.RequireRole("administrator", "user"));
            //});

            services.Configure<DbConnectionSettingsModel>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;

                options.Host = Configuration.GetValue<string>("MongoConnection:Host");
                options.Port = Configuration.GetValue<int>("MongoConnection:Port");
                options.UserName = Configuration.GetValue<string>("MongoConnection:UserName");
                options.Password = Configuration.GetValue<string>("MongoConnection:Password");
                options.UseSsL = Configuration.GetValue<bool>("MongoConnection:UseSsl");
                options.Database = Configuration.GetValue<string>("MongoConnection:Database");
            });

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

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = Configuration.GetSection("IdentityServer:UrlPath").Value,
                AllowedScopes = { "WebApi" },
                RequireHttpsMetadata = false
            });

            // global policy, if assigned here (it could be defined indvidually for each controller) 
            app.UseCors("CorsPolicy");

            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings());

            app.UseStaticFiles();
            app.UseMvc();
        }

        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReplyRepository, ReplyRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
        }

        private static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void InjectOthers(IServiceCollection services)
        {
            services.AddSingleton<DbConnectionSettings>();
            services.AddScoped<DifferencesDbContext>();
        }
    }
}
