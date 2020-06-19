using Unity.Entities;
using Unity.Mathematics;

namespace Prototype.Components.Common
{
  public struct NeedMoveTag : IComponentData
  {
    public float2 Direction;
  }
}