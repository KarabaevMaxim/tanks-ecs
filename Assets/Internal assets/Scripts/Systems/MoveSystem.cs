using Prototype.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Authoring;
using Unity.Transforms;

namespace Prototype.Systems
{
  [AlwaysSynchronizeSystem]
  [UpdateAfter(typeof(InputSystem))]
  public class MoveSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var deltaTime = Time.DeltaTime;
      
      // Entities
      //   .WithAll<PlayerComponent>()
      //   .ForEach((ref Translation translation, ref InputComponent input, ref MoveSpeedComponent moveSpeed) =>
      //   {
      //     var delta = input.Value * moveSpeed.Value * deltaTime;
      //     translation.Value += new float3(delta.x, translation.Value.y, delta.y);
      //   })
      //   .Run();
      
      Entities
        .WithAll<PlayerComponent>()
        .ForEach((ref PhysicsVelocity velocity, ref InputComponent input, ref MoveParamsComponent moveParams) =>
        {
          var newVelocity = velocity.Linear;
          newVelocity.z += input.Value.y * moveParams.MoveSpeedValue * deltaTime;
          newVelocity.x += input.Value.x * moveParams.MoveSpeedValue * deltaTime;
          velocity.Linear = newVelocity;
        })
        .Run();

      return default;
    }
  }
}