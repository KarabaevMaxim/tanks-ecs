using Prototype.Common.Components;
using Prototype.Common.Components.Parts;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.Common.Systems
{
  [AlwaysSynchronizeSystem]
  public class TankAimSystem : SystemBase
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    
    protected override void OnCreate()
    {
      _commandBufferSystem =
        World.DefaultGameObjectInjectionWorld.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
      var commandBuffer = _commandBufferSystem.CreateCommandBuffer().ToConcurrent();
      
      Entities.ForEach((Entity entity, ref Rotation rotation, in int entityInQueryIndex, in TowerComponent tower) =>
      {
        var parent = GetComponent<Parent>(entity);

        if (parent.Value != Entity.Null)
        {
          if (HasComponent<CanHaveTargetComponent>(parent.Value))
          {
            var target = GetComponent<CanHaveTargetComponent>(parent.Value);

            if (target.Value != Entity.Null)
            {
              var parentTranslation = GetComponent<Translation>(parent.Value);
              var targetTranslation = GetComponent<Translation>(target.Value);
              var direction = targetTranslation.Value.xz - parentTranslation.Value.xz;
              
              commandBuffer.AddComponent(entityInQueryIndex, entity, new NeedRotateComponent
              {
                RotationSpeed = tower.RotationSpeed,
                TargetValue = new float3(direction.x, 0.0f, direction.y)
              });
            }
          }
        }
      }).ScheduleParallel();
    }
  }
}