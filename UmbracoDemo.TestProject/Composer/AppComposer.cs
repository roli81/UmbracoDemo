using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoDemo.TestProject.Api.Models;
using UmbracoDemo.TestProject.Components;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Composer
{
    public class AppComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            RegisterServices(builder);
            AddComponents(builder);
        }


        private void AddComponents(IUmbracoBuilder builder)
        {
            builder.Components().Append<TestComponent>();
        }

        private void RegisterServices(IUmbracoBuilder builder)
        {
            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddSingleton<IImageService, ImageService>();
            builder.Services.AddScoped<IWidgetService, WidgetService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddScoped<IMachineService, MachineService>();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyOrigin();

                                  });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Config["Jwt:Issuer"],
                    ValidAudience = builder.Config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Config["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

            });

            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                                                    .RequireAuthenticatedUser().Build());
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

        }
    }
}
