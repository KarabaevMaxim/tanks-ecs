using Unity.Entities;

namespace Prototype.Components
{
  [GenerateAuthoringComponent]
  public struct TargetComponent : IComponentData
  {
    public float SearchRange;
    
    public Entity Value;
  }
}