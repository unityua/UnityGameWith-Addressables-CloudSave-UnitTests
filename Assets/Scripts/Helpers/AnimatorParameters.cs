using UnityEngine;

namespace PesPatron.Helpers
{
    public static class AnimatorParameters
    {
        public static readonly int VelocityX = Animator.StringToHash("VelocityX");
        public static readonly int VelocityZ = Animator.StringToHash("VelocityZ");
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
    }
}