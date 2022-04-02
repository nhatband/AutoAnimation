using Icomm.BrowserAgent;
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
            var resLogin = Login();
            if (resLogin != 2)
                return -1;

            return 0;
        }
        public int Login()
        {
            TestChromeDriver();
            driver.Navigate().GoToUrl("https://www.tiktok.com/vi-VN/");
            var btnLogin = driver.FindElements(By.CssSelector("Button[data-e2e='top-login-button']"));
            if (btnLogin.Count() != 0)
            {
                driver.ClickX(btnLogin[0]);
                Task.Delay(3 * 1000).Wait();
                var frame = driver.FindElement(By.CssSelector("iframe.tiktok-tpndsz-IframeLoginSite"));
                driver.SwitchTo().Frame(frame);
                var loginGG = driver.FindElements(By.CssSelector("div.channel-item-wrapper-2gBWB"));
                if (loginGG.Count() != 0)
                    driver.ClickX(loginGG[3]);
                Console.WriteLine("");
            }
            return -1;
        }
        public void TestChromeDriver()
        {
            driver.Navigate().GoToUrl("https://google.com");
            Task.Delay(2 * 1000).Wait();
            var sigin = driver.FindElementOrDefault(By.CssSelector("a[href='https://accounts.google.com/ServiceLogin?hl=en&passive=true&continue=https://www.google.com/&ec=GAZAmgQ']"));
            if (sigin != null)
                driver.ClickX(sigin);
            Task.Delay(2 * 1000).Wait();
            var edtUser = driver.FindElementOrDefault(By.CssSelector("#identifierId.whsOnd.zHQkBf"));
            if (edtUser != null)
                edtUser.SendKeys("thoithanh26202@gmail.com");
            var btnNext = driver.FindElementOrDefault(By.CssSelector("div.zQJV3 button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ"));
            if (btnNext != null)
                driver.ClickX(btnNext);
            Task.Delay(5 * 1000).Wait();
            var edtPass = driver.FindElementOrDefault(By.CssSelector("input[type='password']"));
            if (edtPass != null)
                edtPass.SendKeys("tung.nguyen1!");
            btnNext = driver.FindElementOrDefault(By.CssSelector("div.zQJV3 button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ"));
            if (btnNext != null)
                driver.ClickX(btnNext);
            Task.Delay(3 * 1000).Wait();
            driver.Navigate().GoToUrl("https://google.com");
        }
    }
}
