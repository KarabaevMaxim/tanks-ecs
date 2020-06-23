using Unity.Entities;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct AttackParamsComponent : IComponentData
  {
    public int Damage;

    public float TimeToAttackInSec;

    public float Range;
  }
}