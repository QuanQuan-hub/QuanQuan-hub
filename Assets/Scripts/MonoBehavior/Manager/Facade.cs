using GamePlay.Manager;
using GamePlay.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : MonoBehaviour
{
    [Tooltip("所有manager的列表")]
    public List<GameObject> ManagersList;
    private void Awake()
    {
        for (int i = 0; i < ManagersList.Count; i++)
        {
            GameObject.Instantiate(ManagersList[i], Vector3.zero, Quaternion.identity);
        }
        //todo : 换成状态机管理
        SceneManager.Instance.LoadScene("Enter", false, ()=> {
            UIManager.Instance.Open(UIDefine.EnterPanel);
        });
    }
}
