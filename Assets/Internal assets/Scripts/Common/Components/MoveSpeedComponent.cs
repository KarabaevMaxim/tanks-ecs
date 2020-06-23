using Unity.Entities;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct MoveSpeedComponent : IComponentData
  {
    public float Value;
  }
}