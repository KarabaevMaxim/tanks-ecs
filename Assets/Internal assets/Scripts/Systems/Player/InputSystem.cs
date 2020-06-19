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
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
      var commandBuffer = ecbSystem.CreateCommandBuffer();
      
      Entities
        .WithAll<PlayerComponent, Translation>()
        .ForEach((Entity entity) =>
        {
          //inputComponent.Direction = input;
          commandBuffer.AddComponent(entity, new NeedMoveTag { Direction = input });
        })
        .Run();
      
      return default;
    }
  }
}