using Prototype.Common.Components;
using Prototype.Infrastructure.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.Common.Systems
{
  public class RotateSystem : SystemBase
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
      _commandBufferSystem =
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
      var deltaTime = Time.DeltaTime;
      var commandBuffer = _commandBufferSystem.CreateCommandBuffer().ToConcurrent();

      Entities.ForEach((Entity entity,
        ref Rotation rotation,
        ref LocalToWorld localToWorld,
        ref NeedRotateComponent needRotate,
        in int entityInQueryIndex) =>
      {
        var targetRotation = quaternion.LookRotationSafe(needRotate.TargetValue, math.up());
        var angle = MathHelper.Angle(rotation.Value, targetRotation);

        if (angle >= 0.05f)
        {
          var worldRotation = math.mul(rotation.Value.value, localToWorld.Value); // world rotation
          
          
           rotation.Value =
             math.slerp(rotation.Value,
               targetRotation,
               needRotate.RotationSpeed * deltaTime);
        }
        else
        {
          commandBuffer.RemoveComponent<NeedRotateComponent>(entityInQueryIndex, entity);
        }
      }).ScheduleParallel();
    }
  }
}