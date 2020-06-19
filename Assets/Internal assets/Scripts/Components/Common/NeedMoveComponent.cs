using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Prototype.Components.Common
{
  [GenerateAuthoringComponent]
  public struct NeedMoveComponent : IComponentData
  {
    [HideInInspector]
    public float3 Direction;
  }
}