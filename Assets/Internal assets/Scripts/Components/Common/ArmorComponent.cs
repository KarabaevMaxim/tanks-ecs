using Unity.Entities;

namespace Prototype.Components.Common
{
  [GenerateAuthoringComponent]
  public struct ArmorComponent : IComponentData
  {
    public int Value;
  }
}