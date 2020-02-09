using AventStack.ExtentReports;
using NessHappyNess.Drivers;
using OpenQA.Selenium;
using System.ComponentModel;


namespace NessHappyNess.Components
{
    public class HappyCube
    {
        public enum E_Cubes
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

        public Status HappyCubes(E_Cubes e_Cubes)
        {
            string a = string.Format("//*[@class = 'categories']/a//div[contains(text(), '{0}')]", EnumsHelper.GetDescription(e_Cubes));
            return SeleniumHelper.OnClick(By.XPath(a));

        }
    }
}
