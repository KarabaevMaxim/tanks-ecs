using Unity.Entities;
using Unity.Mathematics;

namespace Prototype.Common.Components
{
  public struct NeedRotateComponent : IComponentData
  {
    /// <summary>
    /// Поцизия цели поворота.
    /// </summary>
    public float3 TargetValue;
    
    public float RotationSpeed;
  }
}