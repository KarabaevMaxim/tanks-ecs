using Unity.Entities;
using Unity.Mathematics;

namespace Prototype.Common.Components
{
  public struct NeedRotateComponent : IComponentData
  {
    public float2 TargetValue;
  }
}