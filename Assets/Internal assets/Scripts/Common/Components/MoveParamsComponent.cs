using Unity.Entities;
using UnityEngine;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct MoveParamsComponent : IComponentData
  {
    [Header("Скорость в м/с")]
    public float MoveSpeed;

    public float RotationSpeed;
  }
}