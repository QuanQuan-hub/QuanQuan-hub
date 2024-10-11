/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Enter
{
    public partial class UI_EnterPanel : GComponent
    {
        public GGraph m_bg;
        public GButton m_startBtn;
        public const string URL = "ui://ca5kr7uzsgf10";

        public static UI_EnterPanel CreateInstance()
        {
            return (UI_EnterPanel)UIPackage.CreateObject("Enter", "EnterPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
            m_startBtn = (GButton)GetChildAt(1);
        }
    }
}