using Unity.Entities;
using UnityEngine;

namespace Prototype.Projectiles.Components
{
  [GenerateAuthoringComponent]
  public struct SimpleProjectileComponent : IComponentData
  {
    [HideInInspector]
    public int Damage;
  }
}