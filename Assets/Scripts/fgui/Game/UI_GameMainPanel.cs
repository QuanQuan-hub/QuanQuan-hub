/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Game
{
    public partial class UI_GameMainPanel : GComponent
    {
        public GButton m_buildBtn;
        public UI_energy_pro m_energy_pro;
        public const string URL = "ui://cyc8zalcq8kt0";

        public static UI_GameMainPanel CreateInstance()
        {
            return (UI_GameMainPanel)UIPackage.CreateObject("Game", "GameMainPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_buildBtn = (GButton)GetChildAt(0);
            m_energy_pro = (UI_energy_pro)GetChildAt(1);
        }
    }
}