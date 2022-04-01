using Icomm.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace TikTokAutoAnimation
{
    class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {

            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hb, configBuilder) =>
                    configBuilder
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddConfigManagerHttpProvider()
                    .AddCommandLine(args)
                )
                .UseIcommLog()
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    services
                    .AddConfigManager(configuration)

                    .AddHostedService<ServiceMain>();
                    //services
//.AddRefitClient<IYoutubeServices>()
//.ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("HostPort").Get<string>()));
                });
            await builder.RunConsoleAsync();
        }
    }
}
