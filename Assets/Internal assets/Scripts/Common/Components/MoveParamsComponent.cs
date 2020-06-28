using Unity.Entities;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct MoveParamsComponent : IComponentData
  {
    public float MoveSpeed;

    public float RotationSpeed;
  }
}