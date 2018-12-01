using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Services;
using DataAccess;
using DataAccess.DapperModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlackJack.Corang
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

            //services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromSeconds(60);
            //    options.Cookie.HttpOnly = true;
            //});


            services.AddMvc();

            services.AddTransient(r =>new BaseRepository<Game>(
                "Games",
                new SqlConnection(Configuration.GetConnectionString("BlackJackContainer"))
                ));
            services.AddTransient(r => new BaseRepository<Player>(
                "Players",
                new SqlConnection(Configuration.GetConnectionString("BlackJackContainer"))
                ));
            services.AddTransient(r => new BaseRepository<Round>(
                "Rounds",
                new SqlConnection(Configuration.GetConnectionString("BlackJackContainer"))
                ));
            services.AddTransient(r => new BaseRepository<RoundPlayer>(
                "RoundPlayers",
                new SqlConnection(Configuration.GetConnectionString("BlackJackContainer"))
                ));

            services.AddTransient<GameLogicService>();
            services.AddTransient<GameService>();
            services.AddTransient<PlayerService>();
            services.AddTransient<RoundService>();
            services.AddTransient<RoundPlayerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
