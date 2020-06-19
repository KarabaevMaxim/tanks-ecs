using Prototype.Components;
using Prototype.Components.Enemy;
using Unity.Entities;
using Unity.Jobs;

namespace Prototype.Systems
{
  [AlwaysSynchronizeSystem]
  public class EnemiesChangeMoveStateSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var deltaTime = Time.DeltaTime;
      var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
      
      Entities
        .ForEach((Entity entity, ref MoveStatesParamsComponent moveStatesParams, ref MoveState moveState) =>
        {
          moveState.TimeToChangeStateInSec += deltaTime;

          if (moveState.TimeToChangeStateInSec >= moveStatesParams.MoveDurationInSec)
          {
            moveState.TimeToChangeStateInSec = 0.0f;
            // add component
          }
        })
      .Run();
      
      Entities
        .ForEach((Entity entity, ref MoveStatesParamsComponent moveStatesParams, ref DelayState delayState) =>
        {
          delayState.TimeToChangeStateInSec += deltaTime;

          if (delayState.TimeToChangeStateInSec >= moveStatesParams.DelayInSec)
          {
            delayState.TimeToChangeStateInSec = 0.0f;
            // add component
          }
        })
        .Run();
      
      return default;
    }
  }
}