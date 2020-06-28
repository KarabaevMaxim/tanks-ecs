using Prototype.Common.Components;
using Unity.Entities;

namespace Prototype.Common.Systems
{
  public class NeedFindTargetSystem : SystemBase
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    
    protected override void OnCreate()
    {
      _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    
    protected override void OnUpdate()
    {
      var commandBuffer = _commandBufferSystem.CreateCommandBuffer().ToConcurrent();
      
      Entities
        .WithNone<NeedFindTargetTag, NeedMoveComponent>()
        .ForEach((Entity entity, in int entityInQueryIndex, in CanHaveTargetComponent canHaveTarget) =>
        {
          if (canHaveTarget.Value == Entity.Null)
            commandBuffer.AddComponent<NeedFindTargetTag>(entityInQueryIndex, entity);
        })
        .ScheduleParallel();
      
      Entities
        .WithNone<NeedFindTargetTag, NeedMoveComponent>()
        .ForEach((Entity entity, in int entityInQueryIndex, in CanHaveTargetComponent canHaveTarget) =>
        {
          if (canHaveTarget.Value != Entity.Null)
            commandBuffer.RemoveComponent<NeedFindTargetTag>(entityInQueryIndex, entity);
        })
        .ScheduleParallel();
    }
  }
}  