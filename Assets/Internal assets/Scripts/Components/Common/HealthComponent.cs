using Unity.Entities;

namespace Prototype.Components.Common
{
  [GenerateAuthoringComponent]
  public struct HealthComponent : IComponentData
  {
    public int Value;
  }
}