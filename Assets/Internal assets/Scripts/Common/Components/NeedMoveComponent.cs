using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct NeedMoveComponent : IComponentData
  {
    [HideInInspector]
    public float2 Direction;
  }
}