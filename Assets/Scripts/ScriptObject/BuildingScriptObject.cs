using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingScriptObject")]
public class BuildingScriptObject : ScriptableObject
{
    public GameObject prefab;
    public float hp;
}
