using FairyGUI;
using GamePlay.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Manager
{
    public class UIManager : MonoSigleton<UIManager>
    {
        public Camera uiCamera;
        public Transform uiRoot;

        private readonly List<FGuiViewBehaviour> _uiStacks = new();
        private readonly Dictionary<UISortingLayer, List<FGuiViewBehaviour>> _uiGroupDict = new();
        protected override void Awake()
        {
            base.Awake();
            AddPackage();
            Init();
        }
        private void AddPackage()
        {
            //初始划字体
            //FontManager.RegisterFont(FontManager.GetFont("detail"), "detail");
            UIConfig.defaultFont = "Default";
            //自定义Loader
            //UIObjectFactory.SetLoaderExtension(typeof(MyGLoader));

        }
        private void Init()
        {
            foreach (var layer in Enum.GetValues(typeof(UISortingLayer)))
            {
                _uiGroupDict.Add((UISortingLayer)layer, new List<FGuiViewBehaviour>());
            }
        }
        private int _uiUniqueId = 1;
        private readonly Dictionary<int, FGuiViewBehaviour> _uiDict = new Dictionary<int, FGuiViewBehaviour>();
        private FGuiViewBehaviour OpenInternal(GameObject uiGo, string uiName, UISortingLayer uiSortingLayer = UISortingLayer.Default,
            int uiSortingOrder = 0, object uiParam = null)
        {
            var viewBehaviour = uiGo.GetComponent<FGuiViewBehaviour>();
            viewBehaviour.uiName = uiName;
            viewBehaviour.Id = _uiUniqueId++;
            viewBehaviour.uiSortingLayer = uiSortingLayer;
            uiGo.SetActive(true);
            var uiPanel = viewBehaviour.panel;
            if (uiPanel != null)
                uiPanel.SetSortingOrder((int)uiSortingLayer * 1000 + uiSortingOrder * 100 + _uiGroupDict[uiSortingLayer].Count + 1, true);
            _uiStacks.Add(viewBehaviour);
            _uiGroupDict[uiSortingLayer].Add(viewBehaviour);
            _uiDict.Add(viewBehaviour.Id, viewBehaviour);
            viewBehaviour.OnInit(uiParam);
            viewBehaviour.OnOpened();
            return viewBehaviour;
        }
        public void Close(FGuiViewBehaviour viewBehaviour)
        {
            if (viewBehaviour == null)
                return;
            viewBehaviour.OnClosed();
            viewBehaviour.OnDispose();
            _uiDict.Remove(viewBehaviour.Id);
            _uiStacks.Remove(viewBehaviour);
            _uiGroupDict[viewBehaviour.uiSortingLayer].Remove(viewBehaviour);
            UnityEngine.Object.Destroy(viewBehaviour.gameObject);
        }
        //一切使用uiName的只能内部使用，不能对外暴露
        public void Close(UIDefine.UIInfo uiInfo)
        {
            var viewBehaviour = Get(uiInfo.uiName);
            if (viewBehaviour != null)
                Close(viewBehaviour);
        }

        public void Hide(UIDefine.UIInfo uiInfo)
        {
            var viewBehaviour = Get(uiInfo.uiName);
            if (viewBehaviour != null)
                viewBehaviour.HideView();
        }

        public void CloseAllType(UISortingLayer sortingLayer)
        {
            List<FGuiViewBehaviour> popups = new();
            foreach (var fGuiView in _uiStacks)
            {
                if (fGuiView.uiSortingLayer == sortingLayer)
                {
                    popups.Add(fGuiView);
                }
            }

            foreach (var fGuiView in popups)
            {
                Close(fGuiView);
            }
        }

        private FGuiViewBehaviour Get(string uiName)
        {
            for (int i = _uiStacks.Count - 1; i >= 0; i--)
            {
                if (_uiStacks[i].uiName == uiName)
                    return _uiStacks[i];
            }
            return null;
        }

        public FGuiViewBehaviour Get(UIDefine.UIInfo uiInfo)
        {
            return Get(uiInfo.uiName);
        }
        private bool CheckViewExist(FGuiViewBehaviour guiView)
        {
            if (guiView != null && guiView.IsSingle)
            {
                guiView.ShowView();
                return true;
            }

            return false;
        }

        public FGuiViewBehaviour Open(UIDefine.UIInfo uiInfo, object uiParam = null)
        {
            var guiView = Get(uiInfo);
            if (CheckViewExist(guiView))
            {
                return guiView;
            }
            var uiComponent = DynamicCreateUI(uiInfo);
            if (uiComponent != null)
            {
                return OpenInternal(uiComponent.gameObject, uiInfo.uiName, uiInfo.uiSortingLayer, uiInfo.uiSortingOrder, uiParam);
            }
            else
            {
                Debug.LogError("找不到UI：" + uiInfo.uiName);
                return null;
            }
        }

        private FGuiViewBehaviour DynamicCreateUI(UIDefine.UIInfo uiInfo)
        {
            var packageName = uiInfo.packageName;
            var component = uiInfo.componentName;
            var uiGo = UnityEngine.Object.Instantiate(uiRoot.gameObject, uiRoot);
            uiGo.SetActive(true);
            uiGo.name = uiInfo.componentName;
            var uiPanel = uiGo.AddComponentIfNotExist<FairyGUI.UIPanel>();
            uiPanel.packageName = packageName;
            uiPanel.componentName = component;
            // uiPanel.SetSortingOrder(1,true); 
            uiPanel.CreateUI();
            uiPanel.container.fairyBatching = true;
            uiGo.transform.localPosition = Vector3.zero;
            var uiComponent = uiGo.AddComponent(uiInfo.viewType) as FGuiViewBehaviour;
            return uiComponent;
        }
    }
}
