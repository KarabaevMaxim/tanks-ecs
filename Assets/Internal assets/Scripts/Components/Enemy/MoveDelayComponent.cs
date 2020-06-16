using Unity.Entities;

namespace Prototype.Components.Enemy
{
  [GenerateAuthoringComponent]
  public struct MoveDelayComponent : IComponentData
  {
    public float DurationInSec;
    
    public float ValueInSec;
  }
}