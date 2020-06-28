using Unity.Collections;
using Unity.Entities;

namespace Prototype.Common.Components
{
  [GenerateAuthoringComponent]
  public struct MachineComponent : IComponentData
  {
    public Entity Tower;
    
    public Entity Wheel1;
    
    public Entity Wheel2;
    
    public Entity Wheel3;
    
    public Entity Wheel4;
  }
}