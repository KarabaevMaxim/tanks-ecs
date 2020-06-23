using Prototype.Common.Components;
using Prototype.Enemy.Components;
using Prototype.Player.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.Player.Systems
{
  public class FindClosestTargetSystem : ComponentSystem
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    
    protected override void OnCreate()
    {
      base.OnCreate();
      _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
      var buffer = _commandBufferSystem.CreateCommandBuffer();

      Entities
        .WithAll<PlayerComponent, NeedFindTargetTag>()
        .ForEach((Entity player, ref CanHaveTargetComponent canHaveTarget, ref Translation translation) =>
        {
          var closestEnemy = Entity.Null;
          var closestEnemyPosition = float3.zero;
          var playerTranslation = translation;
          var playerCanHaveTarget = canHaveTarget;

          Entities.WithAll<EnemyComponent>()
            .ForEach((Entity enemy, ref Translation enemyTranslation) =>
            {
              var playerToEnemyDistance = math.distance(playerTranslation.Value.xz, enemyTranslation.Value.xz);

              if (playerToEnemyDistance <= playerCanHaveTarget.SearchRange)
              {
                if (closestEnemy == Entity.Null ||
                    math.distance(playerTranslation.Value.xz, closestEnemyPosition.xz) < playerToEnemyDistance)
                {
                  closestEnemy = enemy;
                  closestEnemyPosition = enemyTranslation.Value;
                }
              }
            });

          canHaveTarget.Value = closestEnemy;

          if (canHaveTarget.Value != Entity.Null)
          {
            buffer.RemoveComponent<NeedFindTargetTag>(player);
            buffer.AddComponent<CanAttackTag>(player);
          }
        });
    }
  }
}