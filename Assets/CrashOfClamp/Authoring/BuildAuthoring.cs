using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BuildAuthoring : MonoBehaviour
{

    public GameObject Prefab;
    public class EnemyBaker : Baker<BuildAuthoring>
    {
        public override void Bake(BuildAuthoring authoring)
        {
            var bakingEntity = GetEntity(TransformUsageFlags.WorldSpace);

            BuildData buildData = default;
            buildData.BuildState = BuildState.Idle;
            buildData.Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic);

            AddComponent(bakingEntity, buildData);
        }
    }
}
