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
using Unity.Entities.Serialization;

namespace GamePlay.Manager
{
    [Serializable]
    public class SubSceneCfg
    {
        public string name;
        public EntitySceneReference subScene;
        public override bool Equals(object obj)
        {
            if (obj is string)
            {
                return obj as string == this.name;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class SceneManager : MonoSigleton<SceneManager>
    {
        public override void OnCreate()
        {

        }
        public List<SubSceneCfg> subSceneMap = new();
        private AsyncOperation _loadHandle = null;
        public void LoadScene(string scene, bool allowLoadEnter = false, Action onLoadComplete = null, SubScene subScene = null)
        {
            if (_loadHandle != null)
            {
                Debug.Log(StringFormat.Format("已有场景加载中,场景:{0}加载失败", scene));
                return;
            }
            _loadHandle = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            _loadHandle.allowSceneActivation = allowLoadEnter;
            _loadHandle.completed += (handle) =>
            {
                _loadHandle = null;
                for (int i = 0; i < subSceneMap.Count; i++)
                {
                    if (subSceneMap[i].Equals(scene))
                    {
                        SceneSystem.LoadSceneAsync(
                            World.DefaultGameObjectInjectionWorld.Unmanaged,
                            subSceneMap[i].subScene);
                        break;
                    }
                }
            };
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
