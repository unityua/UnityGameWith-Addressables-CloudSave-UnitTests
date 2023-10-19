using PesPatron.Helpers;
using UnityEngine;

namespace PesPatron.Characters
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterMovement _actorMovement;
        [Space]
        [SerializeField] private float _velocityChangeSpeed = 2f;
        [SerializeField] private float _returnVelocitySpeed = 6f;
        [SerializeField] private float _minVelocityToReturnToIdle = 0.1f;

        private Vector3 _targetVelocity;
        private Vector3 _currentVelocity;

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void ResetAllAnimations()
        {
            _animator.Rebind();
        }

        public void LateUpdate()
        {
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            bool lastFrameSpeedIsZero = _actorMovement.LastFrameSpeed == 0f;

            Vector3 moveDelta = lastFrameSpeedIsZero ? Vector3.zero : _actorMovement.MoveDelta / _actorMovement.LastFrameSpeed;
            moveDelta.y = 0f;

            bool isMoving = moveDelta.magnitude > _minVelocityToReturnToIdle;

            float velocutySpeed = isMoving ? _velocityChangeSpeed : _returnVelocitySpeed;

            _targetVelocity = _actorMovement.RotationMesh.InverseTransformDirection(moveDelta);

            _currentVelocity = Vector3.MoveTowards(_currentVelocity, _targetVelocity, velocutySpeed * Time.deltaTime);

            _animator.SetBool(AnimatorParameters.IsMoving, isMoving);
            _animator.SetFloat(AnimatorParameters.VelocityX, _currentVelocity.x);
            _animator.SetFloat(AnimatorParameters.VelocityZ, _currentVelocity.z);
        }
    }
}