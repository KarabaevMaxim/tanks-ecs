using Prototype.Components;
using Prototype.Components.Common;
using Prototype.Components.Player;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Prototype.Systems.Player
{
  [AlwaysSynchronizeSystem]
  public class InputSystem : JobComponentSystem
  {
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    
    protected override void OnCreate()
    {
      _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      var commandBuffer = _commandBufferSystem.CreateCommandBuffer();
      
      Entities
        .WithAll<PlayerComponent, Translation>()
        .ForEach((Entity entity) =>
        {
          commandBuffer.AddComponent(entity, new NeedMoveComponent { Direction = input });

          if (math.length(input) <= 0.05f)
          {
            commandBuffer.RemoveComponent<MovingTag>(entity);
            commandBuffer.AddComponent<NeedFindTargetTag>(entity);
          }
          else
          {
            commandBuffer.AddComponent<MovingTag>(entity);
            commandBuffer.RemoveComponent<NeedFindTargetTag>(entity);
          }
        })
        .Run();
      
      return default;
    }
  }
}