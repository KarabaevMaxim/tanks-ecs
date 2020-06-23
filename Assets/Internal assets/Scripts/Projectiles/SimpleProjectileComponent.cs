using Unity.Entities;
using UnityEngine;

namespace Prototype.Projectiles
{
  [GenerateAuthoringComponent]
  public struct SimpleProjectileComponent : IComponentData
  {
    [HideInInspector]
    public int Damage;
  }
}