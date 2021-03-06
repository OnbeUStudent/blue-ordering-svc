using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Dii_OrderingSvc.Clients;
using Dii_OrderingSvc.Data;
using Dii_OrderingSvc.Features.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Dii_OrderingSvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string _policyName = "CorsPolicy";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Allow a JWT bearer token but do not validate it.
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        // fool the validation logic
                        SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                        {
                            var jwt = new JwtSecurityToken(token);
                            return jwt;
                        },
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = false,
                        ValidateLifetime = false,
                        RequireExpirationTime = false,
                        RequireSignedTokens = false
                    };
                });
            services.AddHealthChecks();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));
            ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dii_OrderingSvc", Version = "v1" });
            });
            services.AddDbContext<OrderingSvcContext>(options =>
                   options.UseInMemoryDatabase(nameof(OrderingSvcContext)));

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddSingleton(typeof(MovieCatalogSvcClient), serviceProvider =>
            {
                var httpClient = DaprClient.CreateInvokeHttpClient("diimoviecatalogsvc");
                return new MovieCatalogSvcClient(httpClient);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OrderingSvcContext context)
        {
          //  if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dii_OrderingSvc v1"));
            }

            app.UseRouting();
            app.UseCors(_policyName);

            app.UseAuthentication();
            app.UseAuthorization();

            DataSeeding.SeedData(context);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc");
            });
        }
    }
}
