using Unity.Entities;

namespace Prototype.Common.Components.Parts
{
  [GenerateAuthoringComponent]
  public struct TowerComponent : IComponentData
  {
    public float RotationSpeed;
  }
}