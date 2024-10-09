
using Unity.Entities;

public enum BuildState
{
    Idle,
    Select,
    Build
}
public struct BuildData : IComponentData
{
    public Entity Prefab;
    public BuildState BuildState;
}
