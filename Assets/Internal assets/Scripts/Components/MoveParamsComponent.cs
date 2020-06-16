using Unity.Entities;

namespace Prototype.Components
{
  [GenerateAuthoringComponent]
  public struct MoveParamsComponent : IComponentData
  {
    public float MoveSpeedValue;
    
    public float RotationSpeedValue;
  }
}