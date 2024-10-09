
using Unity.Entities;

public enum BuildState
{
    Idle,
    Select,
    Selected,
    Build,
    Building
}
public struct BuildData : IComponentData
{
    public Entity Prefab;
    public BuildState BuildState;
}
