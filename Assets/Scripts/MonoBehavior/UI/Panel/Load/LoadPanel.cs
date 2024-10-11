using Cysharp.Threading.Tasks;
using GamePlay.UI;
using Load;
using System;
using UnityEngine;

public class LoadData
{
    public AsyncOperation loadHandle;
    public Action OnCompleteBack;
}
public class LoadPanel : FGuiViewBehaviourV2<UI_LoadPanel>
{
    private LoadData _loadData;
    public override void OnInit(object uiParam)
    {
        this._loadData = uiParam as LoadData;
        this.fView.m_loadPro.max = 1f;
        this.fView.m_isLoadDone.selectedIndex = 0;
        this.fView.m_pressBtn.onClick.Add(PressContinue);
    }

    public async override void OnOpened()
    {
        await ShowProgress();
    }
    private async UniTask ShowProgress()
    {
        while (!_loadData.loadHandle.isDone)
        {
            float progress = Mathf.Clamp01(_loadData.loadHandle.progress / 0.9f); // 进度范围从0到1

            this.fView.m_loadPro.value = progress;
            if (progress >= 1.0f)
            {
                this.fView.m_isLoadDone.selectedIndex = 1;
            }

            await UniTask.Yield();
        }
        this.fView.m_loadPro.value = this.fView.m_loadPro.max;
        this.fView.m_isLoadDone.selectedIndex = 1;
    }
    private void PressContinue()
    {
        float progress = Mathf.Clamp01(_loadData.loadHandle.progress / 0.9f); // 进度范围从0到1

        if (progress >= 1.0f)
        {
            _loadData.loadHandle.allowSceneActivation = true; // 当进度达到100%时，允许激活场景
            CloseSelf();
            _loadData.OnCompleteBack?.Invoke();
        }
    }
    public override void OnClosed()
    {

    }

    public override void OnDispose()
    {

    }

}
