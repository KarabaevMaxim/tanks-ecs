﻿using Prototype.Common.Components;
using Prototype.Common.Components.AttackTypes;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Prototype.Common.Systems
{
  public class RangeAttackSystem : JobComponentSystem
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
      _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
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
        })
        .Run();
      
      return default;
    }
  }
}