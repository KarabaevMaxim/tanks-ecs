using Prototype.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.Systems
{
  [AlwaysSynchronizeSystem]
  public class RotateToTargetSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
      
      Entities
        .ForEach((ref Translation translation,
          ref Rotation rotation,
          ref TargetComponent target, 
          ref MoveParamsComponent moveParams) =>
        {
          if (target.Value != Entity.Null)
          {
            var targetTranslation = entityManager.GetComponentData<Translation>(target.Value);
            var direction = targetTranslation.Value - translation.Value;
            direction.y = 0f;
            rotation.Value = quaternion.LookRotation(direction, math.up());
          }
        }).Run();
    
      
      return default;
    }
    
    // protected override void OnUpdate()
    // {
    //   Entities
    //     .ForEach((ref Translation translation,
    //       ref Rotation rotation,
    //       ref TargetComponent target, 
    //       ref MoveParamsComponent moveParams) =>
    //     {
    //     });
    //   
    // }
  }
}