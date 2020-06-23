﻿using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Prototype.Projectiles
{
  [GenerateAuthoringComponent]
  public struct TargetedPositionProjectileComponent : IComponentData
  {
    [HideInInspector]
    public int Damage;

    [HideInInspector]
    public float2 TargetPosition;
  }
}