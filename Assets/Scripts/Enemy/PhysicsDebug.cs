using UnityEngine;

namespace Scripts.Enemy
{
    public class PhysicsDebug
    {
        public static void DrawDebug(Vector3 pos, float radius, float seconds)
        {
            Debug.DrawRay(pos, Vector3.up * radius, Color.red, seconds);
            Debug.DrawRay(pos, Vector3.down * radius, Color.red, seconds);
            Debug.DrawRay(pos, Vector3.left * radius, Color.red, seconds);
            Debug.DrawRay(pos, Vector3.right * radius, Color.red, seconds);
            Debug.DrawRay(pos, Vector3.forward * radius, Color.red, seconds);
            Debug.DrawRay(pos, Vector3.back * radius, Color.red, seconds);
        }
    }
}