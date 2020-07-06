using Unity.Entities;
using UnityEngine;

namespace Prototype.Common.Components.Parts
{
  [GenerateAuthoringComponent]
  public struct WheelComponent : IComponentData
  {
    [Header("Диаметр в м")]
    public float Diameter;
  }
}