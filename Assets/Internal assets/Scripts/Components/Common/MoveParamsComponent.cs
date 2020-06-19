using Unity.Entities;

namespace Prototype.Components.Common
{
  [GenerateAuthoringComponent]
  public struct MoveParamsComponent : IComponentData
  {
    public float MoveSpeedValue;
    
    public float RotationSpeedValue;
  }
}