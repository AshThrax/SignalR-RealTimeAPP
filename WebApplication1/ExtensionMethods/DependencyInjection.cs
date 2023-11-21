using Microsoft.AspNetCore.SignalR;
using WebApplication1.SignalR;
namespace WebApplication1.ExtensionMethods
{
    public static  class DependencyInjection
    {
        //ajout de signalR
        public static IServiceCollection AddsignService(this IServiceCollection services,IConfiguration config) 
        {
            services.AddSignalR().AddStackExchangeRedis(configuration => 
            {
                //allow user to distribute message accros multiple instances of our application
                configuration.config = "locahost";//replace with redis serveur 
                configuration.InstanceName = "ChatApp";
            });
            
        }

        // auth0
        public static IServiceCollection Addauth0(this IServiceCollection services,IConfiguration config) 
        { 
            
            return services;
        }
        //----adda application

        public static IApplicationBuilder AddSignalApp(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints => 
            {
                endpoints.MapHub<ChatHub>("/chathub");//map SignalR
            });
            return app;        
        }
    }
}
