using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace Icomm.ForumClient
{
    public class MyChromeDrive
    {


        public IWebDriver InitChromeAsync(string AccountId, long ProxyId, string rootDir)
        {

                var options = new ChromeOptions();
                if (!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }
                if (!Directory.Exists($"{rootDir}\\{AccountId}"))
                {
                    Directory.CreateDirectory($"{rootDir}\\{AccountId}");
                }
                options.AddArgument($"user-data-dir={rootDir}\\{AccountId}");
                //if (proxy.Any())
                //    options.AddArguments($"--proxy-server=http://{proxy.FirstOrDefault().Host}:{proxy.FirstOrDefault().Port}");
                var driver = new ChromeDriver(options);
                return driver;

        }
    }
}