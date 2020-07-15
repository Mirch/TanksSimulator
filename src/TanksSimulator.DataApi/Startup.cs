using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TanksSimulator.DataApi.Data;
using TanksSimulator.Shared.Data;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.DataApi
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
            services.Configure<TanksSimulatorDbSettings>(
               Configuration.GetSection("DatabaseSettings"));

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var databaseSettings = sp.GetRequiredService<IOptions<TanksSimulatorDbSettings>>().Value;
                var client = new MongoClient(databaseSettings.ConnectionString);
                var database = client.GetDatabase(databaseSettings.DatabaseName);

                return database;
            });

            services.AddSingleton<ITanksSimulatorDbSettings>(sp =>
                sp.GetRequiredService<IOptions<TanksSimulatorDbSettings>>().Value);

            services.AddScoped<IRepository<TankModel>, TanksRepository>();
            services.AddScoped<IRepository<GameMapModel>, MapsRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
