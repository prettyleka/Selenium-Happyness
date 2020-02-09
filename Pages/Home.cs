using AventStack.ExtentReports;
using NessHappyNess.Drivers;
using OpenQA.Selenium;


namespace NessHappyNess.Pages
{
    class Home : Basic
    {
        public Status ClickOnHappyness()
        {
            return SeleniumHelper.OnClick(By.XPath("//*[@class = 'firstDiv heb']/p/span[contains(text(), 'Happyness')]"));
        }
    }
}
