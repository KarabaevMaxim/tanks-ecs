using Unity.Entities;
using UnityEngine;

namespace Prototype.Projectiles.Components
{
  [GenerateAuthoringComponent]
  public struct TargetedProjectileComponent : IComponentData
  {
    [HideInInspector]
    public int Damage;

    [HideInInspector]
    public Entity Target;
  }
}