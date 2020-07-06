using Prototype.Common.Components;
using Prototype.Common.Components.Parts;
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
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
      _commandBufferSystem =
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }
    
    protected override void OnUpdate()
    {
      var deltaTime = Time.DeltaTime;
      var commandBuffer = _commandBufferSystem.CreateCommandBuffer();
      
      Entities
        .WithAll<MachineComponent>()
        .ForEach((Entity entity,
          ref Translation translation,
      //    in Rotation rotation,
          in NeedMoveComponent needMove,
          in MoveParamsComponent moveParams,
          in int entityInQueryIndex) =>
        {
          var delta = needMove.Direction * moveParams.MoveSpeed * deltaTime;
          translation.Value.xz += delta;
          var rotDelta = new float3(
            needMove.Direction.x * moveParams.RotationSpeed,
            0, 
            needMove.Direction.y * moveParams.RotationSpeed);

          commandBuffer.AddComponent(entity, new NeedRotateComponent
          {
            RotationSpeed = moveParams.RotationSpeed,
            TargetValue = rotDelta
          });

          var buffer = GetBufferFromEntity<Child>(true);

          foreach (var child in buffer[entity])
          {
            if (HasComponent<WheelComponent>(child.Value))
            {
              var wheel = GetComponent<WheelComponent>(child.Value);

              var wheelRotation = GetComponent<Rotation>(child.Value);
              var wheelRotDelta = (moveParams.MoveSpeed * deltaTime) / (math.PI * wheel.Diameter);

              SetComponent(child.Value, new Rotation
              {
                Value = math.mul(wheelRotation.Value,
                  quaternion.Euler(wheelRotDelta, 0, 0))
              });
            }
          }
        })
        .WithoutBurst()
        .Run();
    }
  }
}