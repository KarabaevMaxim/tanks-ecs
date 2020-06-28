using Prototype.Common.Components;
using Prototype.Common.Components.AttackTypes;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Prototype.Common.Systems
{
  public class RangeAttackSystem : JobComponentSystem
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
      _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
      Enabled = false;
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var buffer = _commandBufferSystem.CreateCommandBuffer();
      var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
      
      // Entities
      //   .WithAll<CanAttackTag>()
      //   .WithNone<AttackReloadingComponent>()
      //   .ForEach((Entity entity, 
      //     ref RangeAttackerComponent rangeAttacker, 
      //     ref CanHaveTargetComponent canHaveTarget,
      //     ref AttackParamsComponent attackParams,
      //     ref Translation translation) =>
      //   {
      //     var targetTranslation = entityManager.GetComponentData<Translation>(canHaveTarget.Value);
      //      var projectile = buffer.Instantiate(rangeAttacker.ProjectilePrefab);
      //      buffer.AddComponent(projectile, new NeedMoveComponent 
      //        { Direction = targetTranslation.Value.xz - translation.Value.xz });
      //      buffer.AddComponent(projectile, new Translation
      //        { Value = new float3(translation.Value.x, translation.Value.y + 0.5f, translation.Value.z) });
      //      buffer.AddComponent<AttackReloadingComponent>(entity);
      //      buffer.RemoveComponent<CanAttackTag>(entity);
      //   })
      //   .Run();
      
      Entities
        .WithAll<CanAttackTag>()
        .WithNone<AttackReloadingComponent>()
        .ForEach((Entity entity, 
          ref RangeAttackerComponent rangeAttacker, 
          ref CanHaveTargetComponent canHaveTarget,
          ref AttackParamsComponent attackParams,
          ref Translation translation) =>
        {
          var targetTranslation = entityManager.GetComponentData<Translation>(canHaveTarget.Value);
          var spawnPoint = entityManager.GetComponentData<Translation>(rangeAttacker.SpawnPoint);
          var projectile = buffer.Instantiate(rangeAttacker.ProjectilePrefab);
          buffer.AddComponent(projectile, new NeedMoveComponent 
            { Direction = targetTranslation.Value.xz - translation.Value.xz });
          buffer.AddComponent(projectile, new Translation
            { Value = spawnPoint.Value });
          buffer.AddComponent<AttackReloadingComponent>(entity);
          buffer.RemoveComponent<CanAttackTag>(entity);
          Debug.Log("Атака");
        })
        .Run();
      
      return default;
    }
  }
}