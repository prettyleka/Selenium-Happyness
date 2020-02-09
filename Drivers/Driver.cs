using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;


namespace NessHappyNess.Drivers

{

    public class DriverManager
    {
        [ThreadStatic]
        private static IWebDriver driver = null;
        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    Create();
                }
                return driver;
            }
            set { driver = value; }
        }



        public static void Create()
        {
            try
            {
                // string driverDir = Consts.Consts.ProjectPath + @"LeumiTradeHelpersLayer\bin\Debug\SeleniumHelpers\Drivers";
                //DesiredCapabilities Capabilities = DesiredCapabilities.Chrome();
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("test-type");
                options.AddArguments("--start-maximized");
                //Capabilities.SetCapability(ChromeOptions.Capability, options);
                // SetDesiredCapabilities(Capabilities);
                Driver = new ChromeDriver(options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(2));
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Window.Maximize();

        }

    }

}