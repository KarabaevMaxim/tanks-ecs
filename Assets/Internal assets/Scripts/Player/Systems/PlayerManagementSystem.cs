using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Prototype.Player.Systems
{
  [AlwaysSynchronizeSystem]
  public class PlayerManagementSystem : JobComponentSystem
  {
    private NativeList<Entity> _players;

    public IEnumerable<Entity> Players => _players;
    
    protected override void OnCreate()
    {
      _players = new NativeList<Entity>(Allocator.Persistent);
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
      return default;
    }
    
    protected override void OnDestroy()
    {
      _players.Dispose();
    }
  }
}