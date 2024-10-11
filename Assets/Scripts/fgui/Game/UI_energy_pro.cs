/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Game
{
    public partial class UI_energy_pro : GComponent
    {
        public GProgressBar m_energy_pro;
        public GLoader m_icon;
        public const string URL = "ui://cyc8zalcq8kt2";

        public static UI_energy_pro CreateInstance()
        {
            return (UI_energy_pro)UIPackage.CreateObject("Game", "energy_pro");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_energy_pro = (GProgressBar)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
        }
    }
}