using Unity.Entities;

namespace Prototype.Components.Common
{
  [GenerateAuthoringComponent]
  public struct AttackParamsComponent : IComponentData
  {
    public int Damage;

    public float Speed;

    public float Range;
  }
}