
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Moster Scriptable Object")]
public class NewBehaviourScript : ScriptableObject
{
    public int ID;
    public List<int> TargetsID;
}
