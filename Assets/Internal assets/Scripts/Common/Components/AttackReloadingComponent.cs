using Unity.Entities;

namespace Prototype.Common.Components
{
  public struct AttackReloadingComponent : IComponentData
  {
    public float ValueInSec;
  }
}