using Icomm.SeleniumClient.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using TextCopy;

namespace Icomm.BrowserAgent
{
    [System.Diagnostics.DebuggerStepThrough]
    public static class ActionHelper
    {
        private const int WAIT_TIMEOUT_DEFAULT = 10;

        public static void SelectDropDown(this IWebElement elementt, string value)
        {
            elementt.SelectElement().SelectByText(value);
        }

        public static SelectElement SelectElement(this IWebElement elementt)
        {
            return new SelectElement(elementt);
        }

        public static IWebElement FindElementOrDefault(this ISearchContext driver, By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public static bool ElementExist(this ISearchContext driver, By by)
        {
            return driver.FindElementOrDefault(by) != null;
        }

        public static IWebElement FindElementWait(this IWebDriver driver, Func<IWebDriver, IWebElement> func, int timeout = WAIT_TIMEOUT_DEFAULT)
        {
            return driver.FindElementWait<IWebElement>(func, timeout);
        }

        public static R FindElementWait<R>(this IWebDriver driver, Func<IWebDriver, R> func, int timeout = WAIT_TIMEOUT_DEFAULT)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(func);
        }

        public static R FindElementWaitOrDefault<R>(this IWebDriver driver, Func<IWebDriver, R> func, int timeout = WAIT_TIMEOUT_DEFAULT)
        {
            try
            {
                return driver.FindElementWait<R>(func, timeout);
            }
            catch (WebDriverTimeoutException)
            {
                return default(R);
            }
        }

        public static void ClickX(this IWebDriver driver, IWebElement element)
        {
            try
            {
                driver.ScrollTo(element);
                element.Click();
            }
            catch (Exception e) when (e is ElementNotVisibleException
                || e is ElementClickInterceptedException
                || e is WebDriverException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            }
        }

        public static void ClickX(this IWebElement element, IWebDriver driver)
        {
            try
            {
                driver.ScrollTo(element);

                Actions actions = new Actions(driver);
                actions.MoveToElement(element);
                actions.Click();
                actions.Perform();
            }
            catch (Exception)
            //when (e is ElementNotVisibleException
            //    || e is ElementClickInterceptedException
            //    || e is WebDriverException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            }
        }

        public static void Input(this IWebElement element, string text)
        {
            //element.Click();
            //Task.Delay(1000).Wait();
            try
            {
                if (text.Length > 150 || text.Contains("http") || text.IndexOfAny(new[] { '\r', '\n' }) != -1)
                    element.InputByCopyPaste(text);
                else
                    element.InputByKeys(text);
            }
            catch (WebDriverException)
            {
                //element.Click();
                element.InputByCopyPaste(text);
            }
        }

        public static void Input(this IWebDriver driver, string text)
        {
            //element.Click();
            //Task.Delay(1000).Wait();
            try
            {
                if (text.Length > 150 || text.Contains("http") || text.IndexOfAny(new[] { '\r', '\n' }) != -1)
                    driver.InputByCopyPaste(text);
                else
                    new Actions(driver).SendKeys(text).Perform();
            }
            catch (WebDriverException)
            {
                //element.Click();
                driver.InputByCopyPaste(text);
            }
        }

        private static void InputByKeys(this IWebElement element, string text)
        {
            element.SendKeys(text);
        }

        private static readonly object _lockCopyPaste = new object();

        private static void InputByCopyPaste(this IWebElement element, string text)
        {
            lock (_lockCopyPaste)
            {
                ClipboardService.SetText(text);
                //var thread = new Thread(() => System.Windows.Forms.Clipboard.SetText(text));
                //thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
                //thread.Start();
                //thread.Join();
                element.SendKeys(OpenQA.Selenium.Keys.Control + 'v');
            }
        }

        private static void InputByCopyPaste(this IWebDriver driver, string text)
        {
            lock (_lockCopyPaste)
            {
                ClipboardService.SetText(text);
                new Actions(driver)
                    .KeyDown(Keys.Control)
                    .KeyDown("v")
                    .KeyUp("v")
                    .KeyUp(Keys.Control)
                    //.SendKeys(Keys.Control + 'v')
                    .Perform();
            }
        }

        //public static object ExecuteScript(this IWebDriver driver, string scripts)
        //{
        //    return ((IJavaScriptExecutor)driver).ExecuteScript(scripts);
        //}

        public static object ExecuteScript(this IWebDriver driver, string scripts, params object[] args)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(scripts, args);
        }

        public static void MouseOver(this IWebDriver driver, IWebElement input)
        {
            const string javaScript = @"var evObj = document.createEvent('MouseEvents');
evObj.initMouseEvent('mouseover',true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
arguments[0].dispatchEvent(evObj);";

            driver.ExecuteScript(javaScript, input);
        }

        public static IWebElement Closest(this IWebElement element, IWebDriver driver, string closestSelector)
        {
            return (IWebElement)driver.ExecuteScript(@"return arguments[0].closest(arguments[1]);", element, closestSelector);
        }

        //public static GoiXuLy()
        //{
        //    var abc = new ABC();
        //    XuLy(abc);
        //}
        //public static GoiXuLy()
        //{
        //    var abc = new ABC();
        //    XuLy(abc);
        //}
        //public static GoiXuLy()
        //{
        //    var abc = new ABC();
        //    XuLy(abc);
        //}
        //public static GoiXuLy()
        //{
        //    var abc = new ABC2();
        //    XuLy(abc);

        //}

        //public static GoiXuLy()
        //{
        //    var abc = new ABC();
        //    XuLy(abc);
        //    abc.So3;
        //}

        //public static void XuLy(ABC so)
        //{
        //    var abc2 = new ac
        //    so.tong();
        //}
    }
}