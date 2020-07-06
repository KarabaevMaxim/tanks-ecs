using Unity.Mathematics;
using Unity.Transforms;

namespace Prototype.Infrastructure.Math
{
  public static class MathHelper
  {
    public static float Dot(quaternion a, quaternion b)
    {
      return (float) ((double) a.value.x * (double) b.value.x + (double) a.value.y * (double) b.value.y +
                      (double) a.value.z * (double) b.value.z + (double) a.value.w * (double) b.value.w);
    }
    
    public static float Angle(quaternion a, quaternion b)
    {
      var num = Dot(a, b);

      return IsEqualUsingDot(num)
        ? 0.0f
        : (float) ((double) math.acos(math.min(math.abs(num), 1f)) * 2.0 * 57.2957801818848);
    }
    
    public static bool IsEqualUsingDot(float dot)
    {
      return (double) dot > 0.999998986721039;
    }
    
    public static quaternion GetWorldRotation(LocalToWorld ltw)
    {
      return quaternion.LookRotationSafe(ltw.Forward, ltw.Up);
    }
  }
}