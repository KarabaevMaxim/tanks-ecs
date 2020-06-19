using Prototype.Components;
using Prototype.Components.Common;
using Prototype.Components.Enemy;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.Systems
{
  [AlwaysSynchronizeSystem]
  public class EnemiesMoveToTargetSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var deltaTime = Time.DeltaTime;
      var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
      
      Entities
        .WithAll<EnemyComponent, MoveState>()
        .WithNone<DelayState>()
        .ForEach((Entity entity, 
          ref Translation translation,
          ref Rotation rotation,
          ref MoveParamsComponent moveParams, 
          ref MoveStatesParamsComponent moveDelayParams,
          ref TargetComponent target,
          ref AttackParamsComponent attackParams) =>
        {
          if (target.Value != Entity.Null)
          {
            var targetTranslation = entityManager.GetComponentData<Translation>(target.Value);
            var needMove = math.distance(translation.Value.xz, targetTranslation.Value.xz) > attackParams.Range;

            if (needMove)
            {
              var delta = math.forward(rotation.Value).xz * moveParams.MoveSpeedValue * deltaTime;
              translation.Value.xz += delta;
            }
            else
            {
              //var commandBuffer = new EntityCommandBuffer(); 
              //commandBuffer.AddComponent<CanAttackTag>(target.Value);
            }
          }
        })
        .Run();

      return default;
    }
  }
}