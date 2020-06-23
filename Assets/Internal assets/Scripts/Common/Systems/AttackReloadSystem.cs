using Prototype.Common.Components;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Prototype.Common.Systems
{
  public class AttackReloadSystem : JobComponentSystem
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    
    protected override void OnCreate()
    {
      _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var buffer = _commandBufferSystem.CreateCommandBuffer();
      var deltaTime = Time.DeltaTime;
      
      Entities
        .ForEach((Entity entity,
          ref AttackParamsComponent attackParams, 
          ref AttackReloadingComponent attackReloading) =>
        {
          if (attackReloading.ValueInSec < attackParams.TimeToAttackInSec)
          {
            attackReloading.ValueInSec += deltaTime;
          }
          else
          {
            Debug.Log("Можно атаковать");
            buffer.AddComponent<CanAttackTag>(entity);
            buffer.RemoveComponent<AttackReloadingComponent>(entity);
          }
        })
        .Run();
      return default;
    }
  }
}