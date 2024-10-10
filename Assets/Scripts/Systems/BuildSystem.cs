using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BuildSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // The system make use of an EntityCommandBuffer, therefore, it needs the system handling the entity command buffer to be initialized to run 
        state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        state.EntityManager.AddComponent<BuildData>(state.SystemHandle);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //EntityCommandBuffer ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        
        foreach (var (mouseAction, buildData) in SystemAPI.Query<RefRO<MouseFollowData>, RefRW<BuildData>>().WithAll<Simulate>())
        {
            if (mouseAction.ValueRO.IsRightClick)
            {
                buildData.ValueRW.BuildState = BuildState.Select;
            }
            if (mouseAction.ValueRO.IsLeftClick)
            {
                if (buildData.ValueRO.BuildState == BuildState.Select)
                {
                    buildData.ValueRW.BuildState = BuildState.Build;
                }
            }
        }
    }
}

public partial struct BuilderSpawnerSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // The system make use of an EntityCommandBuffer, therefore, it needs the system handling the entity command buffer to be initialized to run 
        state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var buildData in SystemAPI.Query<RefRW<BuildData>>().WithAll<Simulate>())
        {
            if (buildData.ValueRO.BuildState == BuildState.Build)
            {
                //开始建造
                //Entity e = ecb.Instantiate(buildData.ValueRO.Prefab);
                //ecb.SetComponent(e, LocalTransform.FromPosition(position.Position));
                buildData.ValueRW.BuildState = BuildState.Idle;
            }
            else if (buildData.ValueRO.BuildState == BuildState.Select)
            {
                //跟着鼠标移动
            }
        }
    }
}