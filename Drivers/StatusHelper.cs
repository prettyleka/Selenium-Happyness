using AventStack.ExtentReports;




namespace NessHappyNess.Drivers

{

    public class StatusHelper

    {

        public static Status ConvertBoolToPassOrFailStatus(bool value)

        {

            return value ? Status.Pass : Status.Fail;

        }

        public static bool ConvertPassOrFailStatusToBool(Status value)

        {

            return value == Status.Pass;

        }



        public static Status OppositeStatus(Status status)

        {

            return status == Status.Fail ? Status.Pass : Status.Fail;

        }

    }

}

