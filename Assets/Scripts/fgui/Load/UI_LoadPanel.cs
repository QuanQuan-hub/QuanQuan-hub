/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Load
{
    public partial class UI_LoadPanel : GComponent
    {
        public Controller m_isLoadDone;
        public GProgressBar m_loadPro;
        public GButton m_pressBtn;
        public GTextField m_loadTxt;
        public const string URL = "ui://cvyri9m3q8kt0";

        public static UI_LoadPanel CreateInstance()
        {
            return (UI_LoadPanel)UIPackage.CreateObject("Load", "LoadPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_isLoadDone = GetControllerAt(0);
            m_loadPro = (GProgressBar)GetChildAt(0);
            m_pressBtn = (GButton)GetChildAt(1);
            m_loadTxt = (GTextField)GetChildAt(2);
        }
    }
}