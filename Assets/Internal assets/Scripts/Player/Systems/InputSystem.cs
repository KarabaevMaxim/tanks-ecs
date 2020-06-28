using Prototype.Common.Components;
using Prototype.Player.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Prototype.Player.Systems
{
  [AlwaysSynchronizeSystem]
  public class InputSystem : SystemBase
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    
    protected override void OnCreate()
    {
      _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
      var input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      var commandBuffer = _commandBufferSystem.CreateCommandBuffer().ToConcurrent();
      
      Entities
        .WithAll<PlayerComponent, Translation>()
        .ForEach((Entity entity, in int entityInQueryIndex) =>
        {

          if (math.length(input) <= 0.05f)
          {
            //commandBuffer.RemoveComponent<MovingTag>(entityInQueryIndex, entity);
            commandBuffer.RemoveComponent<NeedMoveComponent>(entityInQueryIndex, entity);
          }
          else
          {
            commandBuffer.AddComponent(entityInQueryIndex, entity, new NeedMoveComponent { Direction = input });
           // commandBuffer.AddComponent<MovingTag>(entityInQueryIndex, entity);
          }
        })
        .ScheduleParallel();
    }
  }
}