using Prototype.Components;
using Prototype.Components.Common;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Prototype.Systems.Common
{
  [AlwaysSynchronizeSystem]
  public class MoveSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var deltaTime = Time.DeltaTime;

      Entities
        .ForEach((ref Translation translation, ref MoveParamsComponent moveParams, ref NeedMoveTag needMoveTag) =>
        {
          var delta = needMoveTag.Direction * moveParams.MoveSpeedValue * deltaTime;
          translation.Value.xz += delta;
        })
        .Run();

      return default;
    }
  }
}