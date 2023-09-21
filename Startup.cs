using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibrary.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace BookLibrary
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
            try
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[
                            "Jwt:Key"]))
                    });
                services.AddDbContext<BookContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("BooksCon")),ServiceLifetime.Transient);
                services.AddScoped<IBookRepo<Book>, LibraryManager>();
                services.AddScoped<IBR2<Book, Book2>, L2Mggr>();
                services.AddControllers();
                services.AddMvc(opt => opt.EnableEndpointRouting = false);
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

                });
                services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                });
                services.AddControllers();
                services.AddControllersWithViews().AddNewtonsoftJson(
                        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                        .AddNewtonsoftJson(opt => opt.SerializerSettings.ContractResolver
                        = new DefaultContractResolver());
                
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1");
                c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            
            
            app.UseAuthorization();

            //app.UseMvc();

            //app.UseMvc(routes =>
            //{

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller}/{action}/{id?}"

            //        );

            //});

            //app.Run(async (context) => { await context.Response.WriteAsync("Hello"); });
            app.Use(async (context, next) =>
            {
                logger.LogInformation("income req 1 "+ context.Request.Method);
                //await context.Response.WriteAsync("Use Middleware Component 1 \n");
                await next();
                logger.LogInformation("outgo res 1");
            });
            app.Use(async (context, next) =>
            {
                logger.LogInformation("income req 2");
                //await context.Response.WriteAsync("Use Middleware Component 2 \n");
                await next();
                logger.LogInformation("outgo res 2");
            });
			
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
            

        }
    }
}
