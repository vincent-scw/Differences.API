using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Differences.Interaction.Repositories;
using Differences.DataAccess.Repositories;
using Differences.DataAccess;

namespace Differences.Api
{
    public class Startup
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

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                var info = new Info
                {
                    Version = "v1",
                    Title = "Differenciate Them API",
                    TermsOfService = "None"
                };
                options.SingleApiVersion(info);

                options.DescribeAllEnumsAsStrings();
            });

            // Add service and create Policy with options 
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                  builder => builder.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });

            services.Configure<DbConnectionSetting>(options =>
            {
                var dockerMongo  = Environment.GetEnvironmentVariable("MONGO_URL");
                options.ConnectionString = dockerMongo;
                //options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
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

            // global policy, if assigned here (it could be defined indvidually for each controller) 
            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUi();
            app.UseMvc();
        }

        private void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IArticalRepository, ArticalRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
