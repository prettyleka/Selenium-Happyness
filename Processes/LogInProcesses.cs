using AventStack.ExtentReports;
using System.Collections.Generic;
using NessHappyNess.Drivers;
using NessHappyNess.Pages;

namespace NessHappyNess.Processes
{ 
        class LogInProcess
    {
        public static Dictionary<string, Status> LoginToNess()
        {
            Dictionary<string, Status> result = new Dictionary<string, Status>();
            LogIn LI = new LogIn();
            result.Add("Navigate To Home Page", LI.NavigateToHomePage());
            result.Add("Insert User", LI.InsertUser(Configuration.UserName));
            result.Add("Insert Password", LI.InsertPassword(Configuration.Password));
            result.Add("Click Enter", LI.ClickEnter());
            return result;
        }

    }
}
