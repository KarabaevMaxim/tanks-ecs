using Unity.Entities;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct ArmorComponent : IComponentData
  {
    public int Value;
  }
}