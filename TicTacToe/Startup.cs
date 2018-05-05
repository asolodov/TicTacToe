using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicTacToe.Hubs;
using TicTacToe.BL.GameManager.Interfaces;
using TicTacToe.BL.GameManager;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.GameInstance;
using TicTacToe.BL.Users;
using TicTacToe.Infrastructure;
using TicTacToe.Services;

namespace TicTacToe
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMvc();

            services.AddSingleton<IGameManager, GameManager>();

            services.AddSingleton<IGameInstanceStorage, GameInstanceStorage>();
            services.AddSingleton<IUserStorage, UserStorage>();

            services.AddTransient<IUserCommunicationService, UserCommunicationService>();
            services.AddTransient<IGameInstanceFactory, GameInstanceFactory>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSignalR(routes =>
            {
                routes.MapHub<GameHub>("gamehub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            AutoMapperConfig.RegisterMappings();
        }
    }
}
