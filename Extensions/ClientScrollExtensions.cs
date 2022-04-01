using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Icomm.SeleniumClient.Extensions
{
    public static class ClientScrollExtensions
    {
        public static void ScrollToEnd(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 10)");
        }

        public static void ScrollTo(this IWebDriver driver, string css_selector)
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"function findPos(obj) {
    var curtop = 0;
    if (obj.offsetParent) {
        do {
            curtop += obj.offsetTop;
        } while (obj = obj.offsetParent);
    return [curtop];
    }
}");
            sb.AppendLine($"window.scrollTo(0,findPos(document.querySelector(\"{css_selector}\"))-window.innerHeight/3)");
            ((IJavaScriptExecutor)driver).ExecuteScript(sb.ToString());
        }

        public static void ScrollTo(this IWebDriver driver, IWebElement element)
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"function findPos(obj) {
    var curtop = 0;
    if (obj.offsetParent) {
        do {
            curtop += obj.offsetTop;
        } while (obj = obj.offsetParent);
    return [curtop];
    }
}");
            sb.AppendLine($"window.scrollTo(0,findPos(arguments[0])-window.innerHeight/3)");
            ((IJavaScriptExecutor)driver).ExecuteScript(sb.ToString(), element);
        }
    }
}