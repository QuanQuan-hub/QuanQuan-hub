using GamePlay.Manager;
using GamePlay.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class Facade : MonoBehaviour
{
    [Tooltip("所有manager的列表")]
    public List<GameObject> ManagersList;
    private void Awake()
    {

        for (int i = 0; i < ManagersList.Count; i++)
        {
            var mgrGo = Instantiate(ManagersList[i], Vector3.zero, Quaternion.identity);
            if (mgrGo.TryGetComponent<MonoSigleton<MonoBehaviour>>(out var sigleton))
            {
                sigleton.OnCreate();
            }
        }
        //todo : 换成状态机管理
        SceneManager.Instance.LoadScene("Enter", false, ()=> {
            UIManager.Instance.Open(UIDefine.EnterPanel);
        });
    }
}
