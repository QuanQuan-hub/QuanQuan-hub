using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public class MouseFollowerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.EntityManager.AddComponent<MouseFollowData>(state.SystemHandle);
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //EntityCommandBuffer ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        SystemAPI.SetComponent(state.SystemHandle, new MouseFollowData
        {
            Position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0),
            IsLeftClick = Input.GetMouseButton(0),
            IsRightClick = Input.GetMouseButton(1),
        });
    }
}
public struct MouseFollowData : IComponentData
{
    public Vector3 Position;
    public bool IsRightClick;
    public bool IsLeftClick;
}
