using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TikTokAutoAnimation
{
    class ServiceMain : BackgroundService
    {
        private readonly IConfiguration configuration;

        public ServiceMain(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        new ActionViewTikTok(configuration).Execute();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }, stoppingToken);
        }
    }
}
