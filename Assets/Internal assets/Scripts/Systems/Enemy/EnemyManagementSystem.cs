using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Prototype.Systems.Enemy
{
  [AlwaysSynchronizeSystem]
  public class EnemySpawnSystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      return default;
    }
  }
  
  [AlwaysSynchronizeSystem]
  public class EnemyDestroySystem : JobComponentSystem
  {
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      return default;
    }
  }

  [AlwaysSynchronizeSystem]
  public class EnemyManagementSystem : JobComponentSystem
  {
    private NativeList<Entity> _enemies;

    public IEnumerable<Entity> Enemies => _enemies;
    
    protected override void OnCreate()
    {
      _enemies = new NativeList<Entity>(Allocator.Persistent);
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      return default;
    }

    protected override void OnDestroy()
    {
      _enemies.Dispose();
    }
  }
}