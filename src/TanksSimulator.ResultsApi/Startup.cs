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
using TanksSimulator.ResultsApi.Data;
using TanksSimulator.Shared.Data;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.ResultsApi
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

            services.AddScoped<IRepository<GameDataModel>, GameDataRepository>();

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
