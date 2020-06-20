using Unity.Entities;
using UnityEngine;

namespace Prototype.Components.Common
{
  [GenerateAuthoringComponent]
  public struct CanHaveTargetComponent : IComponentData
  {
    public float SearchRange;
    
    [HideInInspector]
    public Entity Value;
  }
}