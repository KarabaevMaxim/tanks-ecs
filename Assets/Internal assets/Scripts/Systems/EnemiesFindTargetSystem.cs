using Prototype.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Prototype.Systems
{
  // [AlwaysSynchronizeSystem]
  public class EnemiesFindTargetSystem : ComponentSystem
  {
    // protected override JobHandle OnUpdate(JobHandle inputDeps)
    // {
    //   Entities
    //     .WithAll<EnemyComponent>()
    //     .ForEach((ref TargetComponent targetComponent, ref Translation translation) =>
    //     {
    //       var closestTarget = Entity.Null;
    //       var closestTargetPosition = float3.zero;
    //       var enemyTranslation = translation.Value;
    //       var maxDistance = targetComponent.SearchRange;
    //       
    //       Entities
    //         .WithAll<PlayerComponent>()
    //         .ForEach((Entity playerEntity, ref Translation playerTranslation) =>
    //         {
    //           if (closestTarget == Entity.Null)
    //           {
    //             closestTarget = playerEntity;
    //             closestTargetPosition = playerTranslation.Value;  
    //           }
    //           else
    //           {
    //             if (math.distance(enemyTranslation, playerTranslation.Value) < maxDistance)
    //             {
    //               if (math.distance(enemyTranslation, playerTranslation.Value) < math.distance(enemyTranslation, closestTargetPosition))
    //               {
    //                 closestTarget = playerEntity;
    //                 closestTargetPosition = playerTranslation.Value; 
    //               }
    //             }
    //           }
    //         })
    //         .Run();
    //
    //       targetComponent.Value = closestTarget;
    //       
    //       if (closestTarget != Entity.Null)
    //         Debug.DrawLine(enemyTranslation, closestTargetPosition);
    //     })
    //     .Run();
    //
    //   return default;
    // }
    
    protected override void OnUpdate()
    {
      Entities
        .WithAll<EnemyComponent>()
        .ForEach((ref TargetComponent targetComponent, ref Translation translation) =>
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
          
          if (closestTarget != Entity.Null)
            Debug.DrawLine(enemyTranslation, closestTargetPosition);
        });
    }
  }
}