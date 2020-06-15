using Unity.Entities;
using Unity.Rendering;

namespace Prototype.Systems
{
  [UpdateInGroup(typeof(InitializationSystemGroup))]
  public class DisableCopySkinnedEntityDataToRenderEntitySystem : ComponentSystem
  {
    protected override void OnCreate()
    {
      World.GetOrCreateSystem<CopySkinnedEntityDataToRenderEntity>().Enabled = false;
    }

    protected override void OnUpdate() { }
  }
}