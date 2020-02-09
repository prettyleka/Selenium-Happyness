using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text.RegularExpressions;

namespace NessHappyNess.Drivers

{

    public class SeleniumHelper

    {

        public static ReadOnlyCollection<string> WindowHandles()

        {

            return DriverManager.Driver.WindowHandles;


        }



        public static bool AcceptAlert()

        {

            try

            {

                DriverManager.Driver.SwitchTo().Alert().Accept();

                return true;

            }

            catch

            {

                return false;

            }

        }



        public static bool FocuseWindowByTitle(String pageTitle, int timeOut)

        {

            try

            {

                var startWait = DateTime.Now;



                do

                {

                    foreach (var window in WindowHandles())

                    {

                        DriverManager.Driver.SwitchTo().Window(window);

                        string sPageTitle = SeleniumHelper.RunCommand("return document.title");

                        if (sPageTitle.Trim() == pageTitle)

                        {

                            return true;

                        }

                    }

                }

                while ((DateTime.Now - startWait).TotalSeconds < timeOut);

                return false;

            }

            catch

            {

                return false;

            }

        }



        public static bool FocuseWindowByPartTitle(String pageTitle, int timeOut)

        {

            try

            {

                var startWait = DateTime.Now;



                do

                {

                    foreach (var window in WindowHandles())

                    {

                        DriverManager.Driver.SwitchTo().Window(window);

                        string sPageTitle = SeleniumHelper.RunCommand("return document.title");

                        if (sPageTitle.Contains(pageTitle))

                        {

                            return true;

                        }

                    }

                }

                while ((DateTime.Now - startWait).TotalSeconds < timeOut);

                return false;

            }

            catch

            {

                return false;

            }

        }



        public static bool CloseWindowByTitle(String pageTitle, int timeOut)

        {

            try

            {

                var startWait = DateTime.Now;



                do

                {

                    foreach (var window in WindowHandles())

                    {

                        DriverManager.Driver.SwitchTo().Window(window);

                        string sPageTitle = SeleniumHelper.RunCommand("return document.title");

                        if (sPageTitle.Trim() == pageTitle)

                        {

                            SeleniumHelper.Close();

                            return true;

                        }

                    }

                }

                while ((DateTime.Now - startWait).TotalSeconds < timeOut);

                return false;

            }

            catch

            {

                return false;

            }

        }



        public static string RunCommand(string command, string sReturn = "return ")

        {

            string sRes = "null";

            Object oObj;

            try

            {

                oObj = (DriverManager.Driver as IJavaScriptExecutor).ExecuteScript(command);

            }

            catch (Exception ex)

            {

                var err = ex.Message;

                return "Error";

            }

            if (oObj != null)

                sRes = oObj.ToString();

            return sRes;

        }



        public static void BackToPerviosWindow()

        {

            DriverManager.Driver.Navigate().Back();

        }



        public static void BackToMainWindow()

        {

            DriverManager.Driver.SwitchTo().Window(DriverManager.Driver.WindowHandles[0]);

        }



        public static bool WaitForCondition(string Condition, int timeoutInSeconds = 10)

        {

            int iCount = 0;

            string sRes = "FALSE";



            do

            {

                sRes = SeleniumHelper.RunCommand(Condition);

                if (sRes.ToUpper() == "TRUE") return true;

                iCount++;

                Thread.Sleep(1000);

            } while (sRes.ToUpper() != "TRUE" && iCount < (timeoutInSeconds * 1000) / 500);



            return sRes.ToUpper() == "TRUE" ? true : false;

        }



        public static bool SwitchToFrame(string frameName)

        {

            var element = FindElement(By.Id(frameName));

            try

            {

                if (element != null)

                {

                    DriverManager.Driver.SwitchTo().Frame(element);

                    return true;

                }

            }

            catch

            {

                return false;

            }

            return false;

        }



        #region Find Element

        public static IWebElement FindElement(By by, int timeoutInSeconds = 40, bool displayed = false)

        {

            IWebElement element = null;

            var wait = new WebDriverWait(DriverManager.Driver, TimeSpan.FromSeconds(timeoutInSeconds));

            try

            {

                element = wait.Until(drv => drv.FindElement(by));

            }

            catch

            {

                return null;

            }

            return element;

        }



        public static IWebElement FindElement(IWebElement elemet, By by)

        {

            IWebElement subElement = null;

            try

            {

                subElement = elemet.FindElement(by);

            }

            catch

            {

                return null;

            }

            return subElement;

        }



        public static ReadOnlyCollection<IWebElement> FindElements(By by, int timeoutInSeconds = 5)

        {

            var wait = new WebDriverWait(DriverManager.Driver, TimeSpan.FromSeconds(timeoutInSeconds));

            try

            {

                return wait.Until(drv => drv.FindElements(by));

            }

            catch { return null; }

        }

        #endregion



        #region Get Text From Element

        public static string GetTextFromElement(IWebElement elemet, int timeOutInSeconds = 10)

        {

            try

            {

                if (elemet == null)

                    return "";

                return elemet.Text;

            }

            catch

            {

                return "";

            }

        }



        public static string GetTextFromElement(By by, int timeOutInSeconds = 10)

        {

            return GetTextFromElement(FindElement(by));

        }



        public static string GetTextFromElement(IWebElement elemet, By by, int timeOutInSeconds = 10)

        {

            return GetTextFromElement(FindElement(elemet, by));

        }

        #endregion



        #region Text Is Correct

        public static Status TextIsCorrect(IWebElement elemet, string text)

        {

            return StatusHelper.ConvertBoolToPassOrFailStatus(GetTextFromElement(elemet).CompareTo(text) == 0);

        }



        public static Status TextIsCorrect(By by, string text)

        {

            return TextIsCorrect(FindElement(by), text);

        }



        public static Status TextIsCorrect(IWebElement elemet, By by, string text)

        {

            return TextIsCorrect(FindElement(elemet, by), text);

        }

        #endregion



        #region Insert Text

        public static Status InsertText(string text, IWebElement elemet, By by, bool WhithChecked = false)

        {

            return InsertText(text, FindElement(elemet, by), WhithChecked);

        }



        public static Status InsertText(string text, By by, bool WhithChecked = false, int timeOutInSeconds = 3)

        {

            return InsertText(text, FindElement(by, timeOutInSeconds), WhithChecked);

        }



        public static Status InsertText(string text, IWebElement elemet, bool WhithChecked = false)

        {

            if (elemet == null)

                return Status.Fail;



            try

            {

                try { elemet.Clear(); }

                catch { }



                elemet.SendKeys(text);



                if (WhithChecked && !(GetTextFromElement(elemet).CompareTo(text) == 0))

                    return Status.Fail;



                return Status.Pass;

            }

            catch { return Status.Fail; }

        }

        #endregion



        #region Element Is Exists

        public static Status ElementIsExists(IWebElement elemet)

        {

            return elemet == null ? Status.Fail : Status.Pass;

        }

        public static Status ElementIsExists(By by, int timeOutInSeconds = 10)

        {

            return FindElement(by, timeOutInSeconds) == null ? Status.Fail : Status.Pass;

        }



        public static Status ElementIsExists(IWebElement elemet, By by)

        {

            return ElementIsExists(FindElement(elemet, by));

        }

        #endregion



        #region On Click

        public static Status OnClick(By by, int timeoutInSeconds = 40)

        {

            return OnClick(FindElement(by, timeoutInSeconds));

        }



        public static Status OnClick(IWebElement element, By by, int timeoutInSeconds = 40)

        {

            return OnClick(FindElement(element, by));

        }



        public static Status OnClick(IWebElement element)

        {

            try

            {

                element.Click();

                WaitDocumentLoading();

                return Status.Pass;

            }

            catch

            {

                return Status.Fail;

            }

        }

        #endregion



        #region Element Attributes

        public static Status AttributeIsContains(By by, EnumsHelper.E_Attributes attributeName, string attributeValue)

        {

            return AttributeIsContains(FindElement(by), attributeName, attributeValue);

        }



        public static Status AttributeIsContains(IWebElement element, EnumsHelper.E_Attributes attributeName, string attributeValue)

        {

            try

            {

                return StatusHelper.ConvertBoolToPassOrFailStatus(element.GetAttribute(EnumsHelper.GetDescription(attributeName)).Contains(attributeValue));

            }

            catch

            {

                return Status.Fail;

            }

        }



        public static Status IconIsContainsInElement(IWebElement element, EnumsHelper.E_IconsClass iconsClass)

        {

            return ElementIsExists(element, By.XPath(".//i[contains(@class,'" + EnumsHelper.GetDescription(iconsClass) + "')]"));

        }



        public static string ElementColor(IWebElement element, By by)

        {

            return FindElement(element, by).GetCssValue("background");//background-color/color

        }

        #endregion



        public static void ScrollToItemByElement(IWebElement element)

        {

            DriverManager.Driver.SwitchTo().DefaultContent();

            if (element != null)

            {

                IJavaScriptExecutor js = (IJavaScriptExecutor)DriverManager.Driver;

                js.ExecuteScript("arguments[0].scrollIntoView();", element);

            }

        }



        public static void ScrollToStart()

        {

            ((IJavaScriptExecutor)DriverManager.Driver).ExecuteScript("window.scrollBy(0,0);");

        }



        public static string CapturePage(string sFileName, int a = 1)

        {

            string screenshot = "";

            bool isScrollContainerExist;



            try

            {

                isScrollContainerExist = bool.Parse((string)((IJavaScriptExecutor)DriverManager.Driver).ExecuteScript(@"return document.getElementById('scrollContainer') != null? ""true"":""false"";"));

            }

            catch

            {

                isScrollContainerExist = bool.Parse((string)((IJavaScriptExecutor)DriverManager.Driver).ExecuteScript(@"return document.getElementById('scrollContainer') != null? ""true"":""false"";"));

            }



            if (isScrollContainerExist == true)

            {

                try

                {

                    Screenshot ss = ((ITakesScreenshot)DriverManager.Driver).GetScreenshot();

                    screenshot = ss.AsBase64EncodedString;

                    byte[] screenshotAsByteArray = ss.AsByteArray;

                    ss.SaveAsFile(sFileName, ScreenshotImageFormat.Png);

                    ss.ToString();

                }

                catch

                {

                    Screenshot ss = ((ITakesScreenshot)DriverManager.Driver).GetScreenshot();

                    screenshot = ss.AsBase64EncodedString;

                    byte[] screenshotAsByteArray = ss.AsByteArray;

                    ss.SaveAsFile(sFileName, ScreenshotImageFormat.Png);

                    ss.ToString();

                }

            }

            else

            {

                try

                {

                    Screenshot ss = ((ITakesScreenshot)DriverManager.Driver).GetScreenshot();

                    screenshot = ss.AsBase64EncodedString;

                    byte[] screenshotAsByteArray = ss.AsByteArray;

                    ss.SaveAsFile(sFileName, ScreenshotImageFormat.Png);

                    ss.ToString();

                }

                catch (Exception e)

                {

                    var err = e.Message;

                }

            }



            return screenshot;

        }



        public static INavigation Navigate()

        {

            return DriverManager.Driver.Navigate();

        }



        #region URL

        public static Status GoToUrl(string url)

        {

            try

            {

                Navigate().GoToUrl(url);

                return Status.Pass;

            }

            catch (Exception g)

            {

                return Status.Fail;

            }

        }



        public static string GetCurrentURL()

        {

            return DriverManager.Driver.Url;

        }



        public static Status UrlIsCorrect(string expectedUrl)

        {

            return StatusHelper.ConvertBoolToPassOrFailStatus(GetCurrentURL().CompareTo(expectedUrl) == 0);

        }



        public static Status UrlEndsWith(string endOfUrl)

        {

            return StatusHelper.ConvertBoolToPassOrFailStatus(GetCurrentURL().EndsWith(endOfUrl));

        }

        #endregion



        public static string GetAttribute(By by, string attribute)

        {



            var element = FindElement(by);

            if (element != null)

                return element.GetAttribute(attribute);

            return "";

        }



        public static void Close()

        {

            if (DriverManager.Driver != null)

                DriverManager.Driver.Close();

        }



        public static void WaitForDocumentLoaded()

        {

            ((IJavaScriptExecutor)DriverManager.Driver).ExecuteScript(@"document.onreadystatechange = function () {if (document.readyState == ""interactive"") {initApplication();  }}");

        }



        public static void QuitDriver()

        {

            try

            {

                if (DriverManager.Driver != null)

                    DriverManager.Driver.Quit();

                DriverManager.Driver = null;

            }

            catch { }

        }



        internal static void BackDriver()

        {

            if (DriverManager.Driver != null)

                DriverManager.Driver.Navigate().Back();

        }





        public static ReadOnlyCollection<IWebElement> FindElements(IWebElement element, By by, int timeOutInSeconds = 5)

        {

            try

            {

                WebDriverWait wait = new WebDriverWait(DriverManager.Driver, TimeSpan.FromSeconds(timeOutInSeconds));

                var lst = element.FindElements(by);

                return lst.Count > 0 ? lst : null;

            }

            catch { return null; }

        }



        public static void WaitDocumentLoading()

        {

            //welcome-overlay ng-isolatescope active  __AjaxWaitImage

            //   DateTime time = DateTime.Now.AddMinutes(4);

            //  for (int i = 1; ElementIsExists(By.XPath("//*[@class='__AjaxWaitImage' or @class='welcome-overlay ng-isolatescope active']")) && DateTime.Now < time; i++) ;

            Thread.Sleep(5000);



            //IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromMinutes(4));

            //wait.Until(driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));

            //wait.Until(d => !ElementIsExists(By.ClassName("__AjaxWaitImage")) && !ElementIsExists(By.ClassName("welcome-overlay ng-isolatescope active")));



            //WaitForCondition(@"return ($(""*[class*='__AjaxWaitImage']"").length==$(""*[class*='__AjaxWaitImage']"").filter("":hidden"").length) || ($(""*[class*='welcome-overlay ng-isolatescope active']"").length==$(""*[class*='welcome-overlay ng-isolatescope active']"").filter("":hidden"").length)? true:false", 30);

        }



        internal static bool checkCheckBox(By by)

        {

            try

            {

                ((IJavaScriptExecutor)DriverManager.Driver)

                .ExecuteScript(

                           "var element=arguments[0]; setTimeout(function() {element.click();},3000)",

                           FindElement(by));

                WaitDocumentLoading();

                return true;

            }

            catch

            {

                return false;

            }

        }



        public static Status TextIsContainsOnElement(By by, string text)

        {

            return StatusHelper.ConvertBoolToPassOrFailStatus(GetTextFromElement(by).Contains(text));

        }

        public static bool TextIsContainsOnElement_boolResult(By by, string text)

        {

            return GetTextFromElement(by).Contains(text);

        }


        public static bool ValidYear(string year)
        {
            string pattern = @"(20+[0-2]+\d)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(year);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidDay(string day)
        {
            string pattern = @"([0-2]\d|3[01])";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(day);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidHebrewDay(string day)
        {
            string pattern = @"([אבגדה][׳])";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(day);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidClock(string Hour)
        {
            string pattern = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(Hour);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ValidTimeShowing(string hourShow)
        {
            string pattern = @"^([1]?[0-9]|2[0-3])\.[0-9][0-9]$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(hourShow);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool ValidDateShowing(string date)
        {
            string pattern = @"([0-9]|[12][0-9]|3[01])[\](0[1-9]|1[012]";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(date);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
