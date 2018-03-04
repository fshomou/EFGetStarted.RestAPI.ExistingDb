﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFGetStarted.RestAPI.ExistingDb.Models;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.UOW;

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

            

            services.AddDbContext<BloggingContext>(options => options.UseSqlServer(connection)).AddUnitOfWork<BloggingContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())

            {

                serviceScope.ServiceProvider.GetService<BloggingContext>().Database.EnsureCreated();

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
