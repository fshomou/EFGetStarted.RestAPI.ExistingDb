﻿using EFGetStarted.RestAPI.ExistingDb.Models;
using EntityFrameWorkUnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace EFGetStarted.RestAPI.ExistingDb
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
            services.AddMvc();
            string connection = @"Server=(localdb)\mssqllocaldb;Database=BloggingGeneric;Trusted_Connection=True;ConnectRetryCount=0";

            //services.AddDbContext<BloggingContext>(options => options.UseSqlServer(connection));
            //services.AddTransient<IBlogRepository, EFGetStarted.RestAPI.ExistingDb.Data.BlogsRepository>();

            //string connection = @"Server=(localdb)\mssqllocaldb;Database=BloggingGeneric;Trusted_Connection=True;ConnectRetryCount=0";
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
            //services.AddTransient<IBlogRepositoryGeneric, EFGetStarted.RestAPI.ExistingDb.GenericData.BlogRepositoryGenerics>();

            services.AddDbContextPool<BloggingContext>(options => options.UseSqlServer(connection)).AddUnitOfWork<BloggingContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())

            {
                serviceScope.ServiceProvider.GetService<BloggingContext>().Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
   
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}