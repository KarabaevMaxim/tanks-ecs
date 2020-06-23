using Unity.Entities;
using UnityEngine;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct CanHaveTargetComponent : IComponentData
  {
    public float SearchRange;
    
    [HideInInspector]
    public Entity Value;
  }
}