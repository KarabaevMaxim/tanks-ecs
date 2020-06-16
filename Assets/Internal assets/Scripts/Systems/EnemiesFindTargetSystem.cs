using Prototype.Components;
using Prototype.Components.Enemy;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Prototype.Systems
{
  public class EnemiesFindTargetSystem : ComponentSystem
  {
    protected override void OnUpdate()
    {
      Entities
        .WithAll<EnemyComponent>()
        .ForEach((ref TargetComponent targetComponent, ref Translation translation) =>
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
                if (closestTarget == Entity.Null)
                {
                  closestTarget = playerEntity;
                  closestTargetPosition = playerTranslation.Value;  
                }
                else
                {
                  if (math.distance(enemyTranslation, playerTranslation.Value) < maxDistance)
                  {
                    if (math.distance(enemyTranslation, playerTranslation.Value) < math.distance(enemyTranslation, closestTargetPosition))
                    {
                      closestTarget = playerEntity;
                      closestTargetPosition = playerTranslation.Value; 
                    }
                  }
                }
              });
            
            targetComponent.Value = closestTarget;
          }
        });
    }
  }
}