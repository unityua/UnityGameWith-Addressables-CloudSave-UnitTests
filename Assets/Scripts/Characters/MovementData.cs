using PesPatron.Helpers;
using UnityEngine;

namespace PesPatron.Characters
{
    [CreateAssetMenu(fileName = "NewMovementData", menuName = ScriptableObjectsPath.DATA + "MovementData")]
    public class MovementData : ScriptableObject
    {
        [Header("Speed")]
        [SerializeField] private float _forwardMoveSpeed = 6f;
        [SerializeField] private float _backwardMoveSpeed = 4f;
        [Header("Rotation")]
        [SerializeField] private float _rotateSpeed = 10f;
        [Header("Gravity")]
        [SerializeField] private float _gravityForce = -10f;
        [SerializeField] private float _groundedGravityForce = -0.1f;

        public float ForwardMoveSpeed => _forwardMoveSpeed;
        public float BackwardMoveSpeed => _backwardMoveSpeed;
        public float RotateSpeed => _rotateSpeed;

        public float GroundedGravityForce => _groundedGravityForce;
        public float GravityForce => _gravityForce;
    }
}