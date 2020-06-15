using Unity.Entities;
using UnityEngine;

namespace Prototype.Infrastructure.Camera
{
  public class LeaderAuthoring : MonoBehaviour, IConvertGameObjectToEntity
  {
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
      var followEntity = FindObjectOfType<CameraFollowToEntity>();

      if (!followEntity)
        Debug.LogError("Не найден объект-цель для камеры");

      followEntity.EntityToFollow = entity;
    }
  }
}