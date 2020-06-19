using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Prototype.Infrastructure.Camera
{
  public class CameraFollowToEntity : MonoBehaviour
  {
    [SerializeField]
    private float3 _offset = float3.zero;
    
    public Entity EntityToFollow { private get; set; }
    
    private EntityManager _entityManager;

    private void Awake()
    {
      _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void LateUpdate()
    {
      if(EntityToFollow == Entity.Null) 
        return;

      var pos = _entityManager.GetComponentData<Translation>(EntityToFollow);
      transform.position = pos.Value + _offset;
    }
  }
}