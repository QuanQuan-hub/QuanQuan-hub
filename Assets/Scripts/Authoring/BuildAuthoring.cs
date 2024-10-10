using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BuildAuthoring : MonoBehaviour
{
    public List<BuildingScriptObject> buildingList;
    public class Baker : Baker<BuildAuthoring>
    {
        public override void Bake(BuildAuthoring authoring)
        {
            var bakingEntity = GetEntity(TransformUsageFlags.None);

            var buffer = AddBuffer<BuildingPrefab>(bakingEntity);
            foreach (var building in authoring.buildingList)
            {
                buffer.Add(new BuildingPrefab() 
                { 
                    Prefab = GetEntity(building.prefab, TransformUsageFlags.Dynamic) ,
                    hp = building.hp
                });
            }
        }
    }
}
