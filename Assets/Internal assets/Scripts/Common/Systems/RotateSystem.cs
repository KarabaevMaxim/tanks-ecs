using Prototype.Common.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.Common.Systems
{
  public class RotateSystem : SystemBase
  {
    protected override void OnUpdate()
    {
      Entities
        .ForEach((Entity entity, 
          NeedRotateComponent needRotateComponent, 
          Rotation rotation,
          Translation translation) =>
        {
          var target = translation.Value.xz + needRotateComponent.TargetValue;
          rotation.Value = quaternion.LookRotationSafe(new float3(target.x, 0, target.y), math.up());
        })
        .ScheduleParallel();
    }
  }
}