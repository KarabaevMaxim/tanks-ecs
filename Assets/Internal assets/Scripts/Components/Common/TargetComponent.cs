using Unity.Entities;

namespace Prototype.Components.Common
{
  [GenerateAuthoringComponent]
  public struct TargetComponent : IComponentData
  {
    public float SearchRange;
    
    public Entity Value;
  }
}