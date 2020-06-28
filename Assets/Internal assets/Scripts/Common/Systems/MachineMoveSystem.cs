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
      var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

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

          var buffer = GetBufferFromEntity<Child>(true);

          foreach (var child in buffer[entity])
          {
            if (entityManager.HasComponent<WheelComponent>(child.Value))
            {
              var wheel = entityManager.GetComponentData<WheelComponent>(child.Value);

              var wheelRotation = entityManager.GetComponentData<Rotation>(child.Value);
              var rotDelta = (moveParams.MoveSpeed * deltaTime) / (math.PI * wheel.Diameter);

              entityManager.SetComponentData(child.Value, new Rotation
              {
                Value = math.mul(wheelRotation.Value,
                  quaternion.Euler(rotDelta, 0, 0))
              });
            }
          }
        })
        .WithoutBurst()
        .Run();
    }
  }
}