﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MoviesAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<MovieAPIDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MovieAPIConnection")));   
                                                            // kiedy instancja MovieAPIDbContext jest wywoływana nie 
                                                            // tworzy nowej instancji tylko najpierw sprawdza  
                                                            // czy juz istnieje w DbContextPool (od 2.0) -> lepsze
            services.AddMvc();

            services.AddSingleton<IMoviesService, MoviesService>();
            services.AddSingleton<IReviewsService, ReviewsService>();

            services.AddScoped<IMovieRepository, MovieRepository>(); 
                                                            // przy każdym request chcemy żeby tworzył nową instancję


            services.AddAutoMapper(
                opt => opt.CreateMissingTypeMaps = true,
                Assembly.GetEntryAssembly());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "Movies API", });
                c.CustomSchemaIds(i => i.FullName);
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "MoviesAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                var swaggerPath = "/swagger/v1/swagger.json";
                c.SwaggerEndpoint(swaggerPath, "Movies API V1");
            });
        }
    }
}
