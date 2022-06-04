using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.IO;

namespace myAspApiTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin",option=>option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllersWithViews().AddNewtonsoftJson(option =>
            option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(option =>
                    option.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddControllers();
            services.AddSwaggerGen(option=> {
                option.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Ticket Api Doc",
                    Description = "Ticket Api Documentation Using Swagger",
                    Version = "V1"
                });

                // this helps to get the file path
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory,fileName);
                option.IncludeXmlComments(filePath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(option=>option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(option=> {
                option.SwaggerEndpoint("swagger/V1/swagger.json","Ticket Api");
                option.RoutePrefix = "";
            });
        }
    }
}
