using Icomm.ForumClient;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTokAutoAnimation
{
    class ActionViewTikTok
    {
        private readonly IConfiguration configuration;
        private IWebDriver driver;

        public ActionViewTikTok(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int Execute()
        {
            Log.Information("Running");
            var RootDir = configuration.GetSection("RootDir").Value;
            var util = new MyChromeDrive();
            driver = util.InitChromeAsync("", 0, RootDir);
            driver.Navigate().GoToUrl("https://www.tiktok.com/vi-VN/");
            var resLogin = Login();
            if (resLogin != 2)
                return -1;

            return 0;
        }
        public int Login()
        {
            return -1;
        }
    }
}
