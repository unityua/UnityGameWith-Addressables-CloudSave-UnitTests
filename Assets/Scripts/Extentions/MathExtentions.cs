using UnityEngine;

namespace PesPatron.Helpers
{
    public static class MathExtentions
    {
        public static float FastAtan2(float y, float x)
        {
            const float ONEQTR_PI = Mathf.PI / 4.0f;
            const float THRQTR_PI = 3.0f * Mathf.PI / 4.0f;
            float r, angle;
            float abs_y = System.Math.Abs(y) + 1e-10f;      // kludge to prevent 0/0 condition

            if (x < 0.0f)
            {
                r = (x + abs_y) / (abs_y - x);
                angle = THRQTR_PI;
            }
            else
            {
                r = (x - abs_y) / (x + abs_y);
                angle = ONEQTR_PI;
            }

            angle += (0.1963f * r * r - 0.9817f) * r;

            if (y < 0.0f)
                return (-angle);     // negate if in quad III or IV
            else
                return (angle);
        }

        public static float Remap(float value, float in1, float in2, float out1, float out2)
        {
            return out1 + (value - in1) * (out2 - out1) / (in2 - in1);
        }

        public static float GetDirection(float current, float target, float treshhold)
        {
            double alwaysLarger = current < target ? (current + 360f) : current;
            double gap = alwaysLarger - target;

            if (gap < treshhold)
                return 0f;

            return gap > 180f ? 1f : -1f;
        }

        public static Vector3 AngleRadToDirection(float angleInRad)
        {
            return new Vector3(Mathf.Cos(angleInRad), 0f, Mathf.Sin(angleInRad));
        }

    }
}