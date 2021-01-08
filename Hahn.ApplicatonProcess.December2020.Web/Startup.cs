using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Askmethat.Aspnet.JsonLocalizer.JsonOptions;
using Aurelia.DotNet;
using AutoMapper;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.Contracts;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.EFCore;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant;
using Hahn.ApplicatonProcess.December2020.Web.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        JsonLocalizationOptions _jsonLocalizationOptions;
        List<CultureInfo> _supportedCultures;
        RequestCulture _defaultRequestCulture;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x => x.Filters.Add(new ValidationFilter()))
            .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TempDb"));

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();

            _jsonLocalizationOptions = Configuration.GetSection(nameof(JsonLocalizationOptions)).Get<JsonLocalizationOptions>();
            _defaultRequestCulture = new RequestCulture(_jsonLocalizationOptions.DefaultCulture,
                _jsonLocalizationOptions.DefaultUICulture);
            _supportedCultures = _jsonLocalizationOptions.SupportedCultureInfos.ToList();

            services.AddJsonLocalization(options =>
            {
                options.ResourcesPath = _jsonLocalizationOptions.ResourcesPath;
                options.UseBaseName = _jsonLocalizationOptions.UseBaseName;
                options.CacheDuration = _jsonLocalizationOptions.CacheDuration;
                options.SupportedCultureInfos = _jsonLocalizationOptions.SupportedCultureInfos;
                options.FileEncoding = _jsonLocalizationOptions.FileEncoding;
                options.IsAbsolutePath = _jsonLocalizationOptions.IsAbsolutePath;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = _defaultRequestCulture;
                options.SupportedCultures = _supportedCultures;
                options.SupportedUICultures = _supportedCultures;
            });

            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IApplicantService, ApplicantService>();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = _defaultRequestCulture,
                SupportedCultures = _supportedCultures,
                SupportedUICultures = _supportedCultures
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DocumentTitle = "Hahn.ApplicationProcess.December2020";
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAureliaCliServer();
                }
            });
        }
    }
}
