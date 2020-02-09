using AventStack.ExtentReports;
using NessHappyNess.Drivers;
using OpenQA.Selenium;


namespace NessHappyNess.Pages
{
    class LogIn
    {
        public Status NavigateToHomePage()
        {
            return SeleniumHelper.GoToUrl("https://homeil.ness.com/");
        }
        public Status InsertPassword(string password)
        {
            return SeleniumHelper.InsertText(password, By.XPath("//input[@id ='password']"));
        }

        public Status InsertUser(string userName)
        {
            return SeleniumHelper.InsertText(userName, By.XPath("//input[@id ='user_name']"));
        }

        public Status ClickEnter()
        {
            return SeleniumHelper.OnClick(By.XPath("//input[@id ='submit_button']"), 40);
        }
    }
}
