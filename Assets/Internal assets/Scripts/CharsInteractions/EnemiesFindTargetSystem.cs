﻿using Prototype.Common.Components;
using Prototype.Enemy.Components;
using Prototype.Player.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.CharsInteractions
{
  public class EnemiesFindTargetSystem : ComponentSystem
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    
    protected override void OnCreate()
    {
      _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
      var commandBuffer = _commandBufferSystem.CreateCommandBuffer();
      
      Entities
        .WithAll<EnemyComponent>()
        .ForEach((Entity enemyEntity, ref CanHaveTargetComponent targetComponent, ref Translation translation, ref Rotation rotation) =>
        {
          if (targetComponent.Value == Entity.Null)
          {
            var closestTarget = Entity.Null;
            var closestTargetPosition = float3.zero;
            var enemyTranslation = translation.Value;
            var maxDistance = targetComponent.SearchRange;

            Entities
              .WithAll<PlayerComponent>()
              .ForEach((Entity playerEntity, ref Translation playerTranslation) =>
              {
                if (math.distance(enemyTranslation, playerTranslation.Value) < maxDistance)
                {
                  if (closestTarget == Entity.Null)
                  {
                    closestTarget = playerEntity;
                    closestTargetPosition = playerTranslation.Value;
                  }
                  else
                  {
                    if (math.distance(enemyTranslation, playerTranslation.Value) <
                        math.distance(enemyTranslation, closestTargetPosition))
                    {
                      closestTarget = playerEntity;
                      closestTargetPosition = playerTranslation.Value;
                    }
                  }
                }
              });

            targetComponent.Value = closestTarget;
          }
          
          var direction = math.forward(rotation.Value);
          commandBuffer.AddComponent(enemyEntity, new NeedMoveComponent { Direction = direction.xz });
          commandBuffer.AddComponent<CanAttackTag>(enemyEntity);
        });
    }
  }
}