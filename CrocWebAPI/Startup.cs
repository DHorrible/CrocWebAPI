using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CrocWebAPI.Npgsql.Context;
using Microsoft.EntityFrameworkCore;
using CrocWebAPI.Npgsql.Helper;
using CrocWebAPI.Npgsql.Manager;

namespace CrocWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            /*
            try
            {
            //    services.AddDbContext<CrocContext>(options =>
            //        options.UseNpgsql(this.Configuration.GetConnectionString("CrocContext")));
            }
            catch (Exception e)
            {

            }
            */
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            while (!NpgsManager.Connection());
        }
    }
}
