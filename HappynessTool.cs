using AventStack.ExtentReports;
using NessHappyNess.Drivers;
using OpenQA.Selenium;
using System.ComponentModel;


namespace NessHappyNess.Components
{
  public  class HappynessTool
    {
            public enum E_Happy
            {
                [Description("דף ראשי")]
                MainPage,
                [Description("אודות")]
                About,
                [Description("כל ההטבות")]
                AllGoodies,
                [Description("הטבות חדשות")]
                NewGoodies,
                [Description("מערכת הטבות לעובדים")]
                SiteSearch
            }

            public Status HappyTool(E_Happy e_Happy)
            {
                string a = string.Format("//*[@class='ness-nav-wrap']//ul//a[contains(text(), '{0}')]", EnumsHelper.GetDescription(e_Happy));
                return SeleniumHelper.OnClick(By.XPath(a));

            }
        public Status EnterForTheSearch(string input)
        {
            return SeleniumHelper.InsertText(input, By.XPath("//input[@id = 'searchtextbox']"));
        }
   }
}
