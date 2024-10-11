using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniFramework.Utility;
using GamePlay.UI;
using Cysharp.Threading.Tasks;
using System;
using Unity.Scenes;
using Unity.Entities;
using UnityEngine.SceneManagement;

namespace GamePlay.Manager
{
    public class SceneManager : MonoSigleton<SceneManager>
    {
        private AsyncOperation _loadHandle;
        public void LoadScene(string scene, bool allowLoadEnter = false, Action onLoadComplete = null, SubScene subScene = null)
        {
            if (_loadHandle != null)
            {
                Debug.Log(StringFormat.Format("已有场景加载中,场景:{0}加载失败", scene));
                return;
            }
            _loadHandle = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            _loadHandle.allowSceneActivation = allowLoadEnter;
            DisplayLoadPanel(_loadHandle, onLoadComplete);
            if (subScene != null)
            {
                SceneSystem.UnloadScene(World.DefaultGameObjectInjectionWorld.Unmanaged, subScene.SceneGUID,
                    SceneSystem.UnloadParameters.DestroyMetaEntities);
            }
        }
        private void DisplayLoadPanel(AsyncOperation handle, Action onLoadComplete = null)
        {
            UIManager.Instance.Open(UIDefine.LoadPanel, new LoadData
            {
                loadHandle = handle,
                OnCompleteBack = onLoadComplete
            });
        }
    }
}
