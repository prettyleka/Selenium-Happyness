using NessHappyNess.Components;

namespace NessHappyNess.Pages
{
    public class Basic
    {
        private HappyCube happyCube = null;
        private HappynessTool happynessTool = null;
        private SideHappyBar sideHappyBar = null;


        #region Properties
        public HappyCube HappyCube
        {
            get
            {
                if (happyCube == null)
                {

                    happyCube = new HappyCube();
                }

                return new HappyCube();
            }
        }
        public HappynessTool HappynessTool
        {
            get
            {
                if (happynessTool == null)
                {
                    happynessTool = new HappynessTool();
                }
                return new HappynessTool();
            }
        }
        public SideHappyBar SideHappyBar
        {
            get
            {
                if (sideHappyBar == null)
                {
                    sideHappyBar = new SideHappyBar();
                }
                return new SideHappyBar();
            }
        }
        #endregion
    }
}
