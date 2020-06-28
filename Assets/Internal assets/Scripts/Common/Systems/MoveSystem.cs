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
        .WithNone<MachineComponent>()
        .ForEach((ref Translation translation,
          ref MoveParamsComponent moveParams, 
          ref NeedMoveComponent needMoveTag) =>
        {
          var delta = needMoveTag.Direction * moveParams.MoveSpeed * deltaTime;
          translation.Value.xz += delta;
        })
        .Run();

      return default;
    }
  }
}