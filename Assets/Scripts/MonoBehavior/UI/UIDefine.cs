using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.UI
{
    public static class UIDefine
    {
        public class UIInfo
        {
            public readonly string packageName;
            public readonly string componentName;
            public readonly Type viewType;
            public readonly UISortingLayer uiSortingLayer;
            public readonly int uiSortingOrder;
            public string uiName => $"{packageName}.{componentName}";

            public UIInfo(string packageName, string componentName, Type viewType,
                UISortingLayer uiSortingLayer = UISortingLayer.Default, int uiSortingOrder = 0)
            {
                this.packageName = packageName;
                this.componentName = componentName;
                this.viewType = viewType;
                this.uiSortingLayer = uiSortingLayer;
                this.uiSortingOrder = uiSortingOrder;
            }
        }

        //
        public static readonly UIInfo GameMainPanel = new("Game", "GameMainPanel", typeof(GameMainPanel));
        public static readonly UIInfo EnterPanel = new("Enter", "EnterPanel", typeof(EnterPanel));
        public static readonly UIInfo LoadPanel = new("Load", "LoadPanel", typeof(LoadPanel));
    }
    public enum UISortingLayer
    {
        Default,
        Popup,
        Top,
        Quit
    }
}