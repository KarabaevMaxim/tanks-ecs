using Unity.Entities;

namespace Prototype.Common.Components.AttackTypes
{
  [GenerateAuthoringComponent]
  public struct RangeAttackerComponent : IComponentData
  {
    public Entity ProjectilePrefab;
  }
}