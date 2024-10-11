using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.UI
{
    public abstract class FGuiViewBehaviourV2<T> : FGuiViewBehaviour where T : GComponent
    {
        private T _fView;
        public T fView => _fView ??= fGui as T;
    }
    public abstract class FGuiViewBehaviour : MonoBehaviour
    {
        public UISortingLayer uiSortingLayer;

        private GComponent _fGui;
        protected GComponent fGui => _fGui ?? (panel.ui);

        private UIPanel _panel;
        public UIPanel panel
        {
            get
            {
                if (_panel == null)
                {
                    _panel = GetComponent<UIPanel>();
                }
                return _panel;
            }
        }

        public string uiName;

        public virtual bool IsSingle => false;
        public int Id;
        public abstract void OnInit(object uiParam); //首次调用
        public abstract void OnOpened(); //每次打开调用
        public abstract void OnClosed(); //每次关闭调用
        public abstract void OnDispose(); //销毁调用

        public void HideView()
        {
            OnHide();
            gameObject.SetActive(false);
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        protected virtual void OnShow()
        {

        }

        protected virtual void OnHide()
        {

        }
        public void CloseSelf()
        {
            Manager.UIManager.Instance.Close(this);
        }
        protected void DynamicCreateUI(string componentName, int sortingOrder = 0)
        {
            var uiPanel = GetComponent<FairyGUI.UIPanel>();
            uiPanel.componentName = componentName;
            uiPanel.SetSortingOrder(sortingOrder, true);
            uiPanel.CreateUI();
        }

    }
}
