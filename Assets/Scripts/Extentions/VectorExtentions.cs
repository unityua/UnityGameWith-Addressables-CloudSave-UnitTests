using UnityEngine;

namespace PesPatron.Helpers
{
    public static class VectorExtentions
    {
        public static Vector3 WithX(this Vector3 vector, float valueX)
        {
            vector.x = valueX;
            return vector;
        }

        public static Vector3 WithY(this Vector3 vector, float valueY)
        {
            vector.y = valueY;
            return vector;
        }

        public static Vector3 WithZ(this Vector3 vector, float valueZ)
        {
            vector.z = valueZ;
            return vector;
        }

        public static Vector3 RandomDirectionXZ()
        {
            Vector2 result = Random.onUnitSphere;

            return new Vector3(result.x, 0f, result.y);
        }
    }
}