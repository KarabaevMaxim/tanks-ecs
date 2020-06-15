using Unity.Entities;

namespace Prototype.Components
{
  [GenerateAuthoringComponent]
  public struct MoveSpeedComponent : IComponentData
  {
    public float Value;
  }
}