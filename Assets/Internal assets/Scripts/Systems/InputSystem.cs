using Prototype.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Prototype.Systems
{
  [AlwaysSynchronizeSystem]
  public class InputSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      var input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      
      Entities
        .WithAll<PlayerComponent, Translation>()
        .ForEach((ref InputComponent inputComponent) =>
        {
          inputComponent.Value = input;
        })
        .Run();
      
      return default;
    }
  }
}