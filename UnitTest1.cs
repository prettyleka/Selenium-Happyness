using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NessHappyNess.Pages;
using NessHappyNess.Components;
using NessHappyNess.Processes;
using NessHappyNess.Drivers;
using OpenQA.Selenium;

namespace NessHappyNess
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Home H = new Home();
            LogInProcess.LoginToNess();
            H.ClickOnHappyness();
            H.HappynessTool.HappyTool(HappynessTool.E_Happy.About);
            H.HappynessTool.HappyTool(HappynessTool.E_Happy.AllGoodies);
            H.HappynessTool.HappyTool(HappynessTool.E_Happy.MainPage);
            H.HappynessTool.HappyTool(HappynessTool.E_Happy.NewGoodies);
            H.HappynessTool.EnterForTheSearch("something");
            H.HappynessTool.HappyTool(HappynessTool.E_Happy.MainPage);
            H.HappyCube.HappyCubes(HappyCube.E_Cubes.BenefitClubs);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.BenefitClubs);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.ChildrenPlace);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.Culture);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.Financial);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.HitechZone);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.HotClub);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.NewVouchersGeneration);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.RankSite);
            H.SideHappyBar.HappyTool(SideHappyBar.E_Side.Vouchers);




            SeleniumHelper.QuitDriver();
        }
    }
}