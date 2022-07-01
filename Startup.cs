using MailMarkup.BackgroundWorkers;
using MailMarkup.Cache;
using MailMarkup.DataAccess;
using MailMarkup.Middleware;
using MailMarkup.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MailMarkup
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
            services.AddOptions();
            services.Configure<MailMarkupConfiguration>(Configuration.GetSection(nameof(MailMarkupConfiguration)));

            services.AddDbContext<StekContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StekConnection")));
            services.AddRazorPages().AddRazorPagesOptions(o => o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

            services.AddSingleton<IServiceCache, ServiceCache>();
            services.AddScoped<IStekRepository, StekRepository>();
            services.AddHostedService<CacheUpdater>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}