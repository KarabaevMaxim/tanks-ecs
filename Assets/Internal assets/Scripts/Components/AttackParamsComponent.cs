using Unity.Entities;

namespace Prototype.Components
{
  [GenerateAuthoringComponent]
  public struct AttackParamsComponent : IComponentData
  {
    public int Damage;

    public float Speed;
  }
}