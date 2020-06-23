using Prototype.Common.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Prototype.Common.Systems
{
  [AlwaysSynchronizeSystem]
  public class MoveSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var deltaTime = Time.DeltaTime;

      Entities
        .ForEach((ref Translation translation, ref MoveSpeedComponent moveParams, ref NeedMoveComponent needMoveTag) =>
        {
          var delta = needMoveTag.Direction * moveParams.Value * deltaTime;
          translation.Value.xz += delta;
        })
        .Run();

      return default;
    }
  }
}