using Prototype.Components;
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
        .WithAll<EnemyComponent>()
        .ForEach((Entity entity, 
          ref Translation translation, 
          ref MoveParamsComponent moveParams, 
          ref MoveDelayComponent moveDelay, 
          ref TargetComponent target,
          ref AttackParamsComponent attackParams) =>
        {
          if (target.Value != Entity.Null)
          {
            var targetTranslation = entityManager.GetComponentData<Translation>(target.Value);
            var needMove = math.distance(translation.Value.xz, targetTranslation.Value.xz) > attackParams.Range;

            if (needMove)
            {
              var dir = math.normalize(targetTranslation.Value.xz - translation.Value.xz);
              translation.Value.xz += dir * moveParams.MoveSpeedValue * deltaTime;
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