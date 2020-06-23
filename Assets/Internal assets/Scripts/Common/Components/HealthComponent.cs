using Unity.Entities;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct HealthComponent : IComponentData
  {
    public int Value;
  }
}