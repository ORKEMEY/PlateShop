using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.DAL;
using Shop.BLL.Interfaces;
using Shop.BLL.Infrastructure;
using Shop.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = Configuration["SpaStaticFiles"];
			});

			services.AddTransient<IOrderService, OrderService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IGoodService, GoodService>();

			services.AddDbContext<ShopContext>(optionsBuilder =>
			{
				optionsBuilder.UseSqlServer(
					new ServiceModule(Configuration).GetConnectionStringFromConfig()
					);
			});
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			//new ServiceModule(Configuration).GetDbContextOptions()
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				   .AddJwtBearer(options =>
				   {
					   options.RequireHttpsMetadata = false;
					   options.TokenValidationParameters = new TokenValidationParameters
					   {
						   ValidateIssuer = true,
						   ValidIssuer = AuthOptions.ISSUER,

						   ValidateAudience = true,
						   ValidAudience = AuthOptions.AUDIENCE,
						   ValidateLifetime = true,

						   IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
						   ValidateIssuerSigningKey = true,
					   };
				   });
			services.AddControllersWithViews();
			services.AddSwaggerGen(config =>
			{
				config.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Shop API",
					Description = "ASP.NET Core Web API"
				});
				config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "JWT token",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				config.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
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

				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop system API V1");
				});
			}


			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseRouting();

			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSpa(spa =>
			{

				spa.Options.SourcePath = Configuration["SpaRoot"];

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});

		}
	}
}
