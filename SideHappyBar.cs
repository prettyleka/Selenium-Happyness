using AventStack.ExtentReports;
using NessHappyNess.Drivers;
using OpenQA.Selenium;
using System.ComponentModel;


namespace NessHappyNess.Components
{
    public class SideHappyBar
    {
        public enum E_Side
        {
            [Description("מועדוני הטבות ")]
            BenefitClubs,
            [Description("שוברים ותווי קניה")]
            Vouchers,
            [Description("עולם הילדים")]
            ChildrenPlace,
            [Description("תרבות ופנאי")]
            Culture,
            [Description("פיננסי")]
            Financial,
            [Description("אתר שוברים של דור חדש בשוברים")]
            NewVouchersGeneration,
            [Description("אתר הייטק זון")]
            HitechZone,
            [Description("מועדון הוט")]
            HotClub,
            [Description("אתר מדרג")]
            RankSite
        }

        public Status HappyTool(E_Side e_Side)
        {
            string a = string.Format("//*[@class = 'categorydescwrap' or @class = 'categorydescwrap special']/*[contains(., '{0}')]", EnumsHelper.GetDescription(e_Side));
            return SeleniumHelper.OnClick(By.XPath(a));
        }
    }
}
