using Prototype.Common.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace Prototype.Common.Systems
{
  [AlwaysSynchronizeSystem]
  [UpdateBefore(typeof(BuildPhysicsWorld))]
  public class MachineMoveSystem : SystemBase
  {
    protected override void OnUpdate()
    {
      var deltaTime = Time.DeltaTime;
  
      Entities
        .WithAll<MachineComponent>()
        .ForEach((Entity entity, 
          ref Translation translation, 
          ref Rotation rotation,
          in NeedMoveComponent needMove, 
          in MoveParamsComponent moveParams) =>
      {
        var delta = needMove.Direction * moveParams.MoveSpeed * deltaTime;
        translation.Value.xz += delta;
        rotation.Value = quaternion.LookRotationSafe(
          new float3(needMove.Direction.x, 0, needMove.Direction.y), math.up());
        
      }).ScheduleParallel();
    }
  }
}