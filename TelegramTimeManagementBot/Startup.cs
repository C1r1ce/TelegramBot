using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TelegramTimeManagementBot.Services;
using TelegramTimeManagementBot.Services.Db;
using TelegramTimeManagementBot;

namespace TelegramTimeManagementBot
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
            services.AddDbContext<ApplicationContext>();

            var botOptions = Configuration.GetSection("BotOptions");
            services.Configure<BotOptions>(botOptions);
            services.AddTransient<DbActivitiesService>();
            services.AddTransient<DbUserActionsService>();
            services.AddTransient<DbUserService>();
            services.AddTransient<CommandService>();
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();

            services
                .AddControllers()
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
