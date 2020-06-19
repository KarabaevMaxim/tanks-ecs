using Unity.Entities;
using UnityEngine;

namespace Prototype.Components.Enemy
{
  [GenerateAuthoringComponent]
  public struct MoveStatesParamsComponent : IComponentData
  {
    public float MoveDurationInSec;
    
    public float DelayInSec;
  }

  public struct MoveState : IComponentData
  {
    public float TimeToChangeStateInSec;
  }
  
  public struct DelayState : IComponentData
  {
    public float TimeToChangeStateInSec;
  }
}