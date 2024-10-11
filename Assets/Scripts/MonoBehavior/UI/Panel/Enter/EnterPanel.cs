using Enter;
using GamePlay.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPanel : FGuiViewBehaviourV2<UI_EnterPanel>
{
    public override void OnClosed()
    {

    }

    public override void OnDispose()
    {

    }

    public override void OnInit(object uiParam)
    {
        this.fView.m_startBtn.onClick.Add(OnClickStart);
    }
    private void OnClickStart()
    {

    }
    public override void OnOpened()
    {

    }
}
