using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Prototype.Components
{
  [GenerateAuthoringComponent]
  public struct InputComponent : IComponentData
  {
    [HideInInspector]
    public float2 Value;
  }
}