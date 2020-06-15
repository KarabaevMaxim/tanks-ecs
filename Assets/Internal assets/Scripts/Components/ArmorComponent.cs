using Unity.Entities;

namespace Prototype.Components
{
  [GenerateAuthoringComponent]
  public struct ArmorComponent : IComponentData
  {
    public int Value;
  }
}