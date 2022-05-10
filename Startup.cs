using System;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

using newWebAPI.Services;
using newWebAPI.MiddleWares;
using newWebAPI.Context;
using newWebAPI.Models;
using Microsoft.AspNetCore.OData;

namespace newWebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config => 
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.EnableEndpointRouting = false;
            })
            .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddControllers().AddOData(opt => opt.Select().Expand().Filter().OrderBy());

            services.AddSingleton<IUserDataService,UserDataService>();
            services.AddCors(p => {
                p.AddPolicy("MyPolicy", builder => {
                    builder
                    .AllowAnyHeader()
                    .WithOrigins("http://127.0.0.1:5500")
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .Build();
                });
            });

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidIssuer = "",
                    ValidAudience = "",
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddDbContext<ApiAppContext>(options =>
                options.UseInMemoryDatabase("DataDB"));

            //services.AddDbContext<ApiAppContext>(options =>
            //    options.UseSqlServer("data source=Localhost; initial catalog=TutorialDB; Integrated Security=SSPI;"));

            services.AddResponseCaching();
            
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A ASP.NET Api example",
                    TermsOfService = new Uri("https://example.com/terms"),
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c => c.SerializeAsV2 = true);
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCaching();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStatusMiddleWare();
        }
    }
}
