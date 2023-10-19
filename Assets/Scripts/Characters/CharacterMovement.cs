using PesPatron.Helpers;
using System;
using UnityEngine;

namespace PesPatron.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private MovementData _movementData;
        [Space]
        [SerializeField] private CharacterController _charaterController;
        [SerializeField] private GroundDetector _groundDetector;
        [SerializeField] private Transform _rotationMesh;
        [Space]
        [SerializeField] private float _speedMultiplyer = 1f;
        [Space]
        [SerializeField] private float _moveThreshold = 0.1f;
        [SerializeField] private float _minMagnitude = 0.15f;

        private float _angleDifference;

        private bool _initialized;
        private Vector3 _moveDirection;
        private float _lastFrameSpeed;
        private Quaternion _targetRotation;

        private Vector3 _moveDelta;
        private float _gravityForce;

        public Vector3 MoveDelta => _moveDelta;
        public Quaternion CurrentRotation => _rotationMesh.rotation;

        public float ForwardMoveSpeed => _movementData.ForwardMoveSpeed;

        public Transform RotationMesh => _rotationMesh;

        public float LastFrameSpeed => _lastFrameSpeed;

        public float SpeedMultiplyer { get => _speedMultiplyer; set => _speedMultiplyer = value; }
        public float AngleDifference => _angleDifference;

        public event Action<CharacterMovement> BecameEnabled;
        public event Action<CharacterMovement> BecameDisabled;

        private const float MIN_T = 0;
        private const float MAX_T = 1;

        private const float MIN_DOT = -1;
        private const float MAX_DOT = 1;

        public void Construct(MovementData movementData)
        {
            _movementData = movementData;
        }

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            BecameEnabled = null;
            BecameDisabled = null;
        }

        private void OnEnable() => BecameEnabled?.Invoke(this);

        private void OnDisable() => BecameDisabled?.Invoke(this);

        private void Update()
        {
            UpdateMove();

            RotateTowardsTarget();
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            Quaternion meshRotation = _rotationMesh.rotation;

            transform.rotation = Quaternion.identity;
            _rotationMesh.rotation = meshRotation;

            _targetRotation = meshRotation;
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            if (enabled)
                _moveDirection = moveDirection;
        }

        public void ResetMoveDirection()
        {
            _moveDirection = Vector3.zero;
            _moveDelta = Vector3.zero;
        }

        public void SetRotation(Quaternion rotation)
        {
            _rotationMesh.rotation = rotation;
            _targetRotation = rotation;
        }

        public void SetCharacterControllerEnabled(bool value)
        {
            _charaterController.enabled = value;
        }

        private void UpdateMove()
        {
            if (_moveDirection.magnitude <= _moveThreshold || enabled == false)
            {
                _moveDirection = Vector3.zero;
                _moveDelta = Vector3.zero;
                return;
            }

            float newMagnitude = MathExtentions.Remap(_moveDirection.magnitude, MIN_T, MAX_T, _minMagnitude, MAX_T);

            Vector3 moveDirectionNormalized = _moveDirection.normalized;

            _moveDirection = moveDirectionNormalized * newMagnitude;

            CalculateTargetRotation(moveDirectionNormalized);

            float moveSpeed = CalculateMoveSpeed(moveDirectionNormalized, (_rotationMesh.rotation * Vector3.forward).normalized);

            _lastFrameSpeed = moveSpeed;
            _moveDelta = _moveDirection * moveSpeed;
            _moveDelta.y += CurrentGravityForce();

            _charaterController.Move(_moveDelta * Time.deltaTime);
            _moveDirection = Vector3.zero;
        }

        private void RotateTowardsTarget()
        {
            Quaternion currentRotation = _rotationMesh.rotation;

            if (currentRotation == _targetRotation)
            {
                _angleDifference = 0f;
                return;
            }

            Quaternion newRotation = Quaternion.Lerp(currentRotation, _targetRotation, _movementData.RotateSpeed * Time.deltaTime);

            _rotationMesh.rotation = newRotation;

            Vector3 oldDirection = currentRotation * Vector3.forward;
            Vector3 newDirection = newRotation * Vector3.forward;

            CalculateAngleDifferenec(oldDirection, newDirection);
        }

        private float CalculateMoveSpeed(Vector3 moveDirectionNormalized, Vector3 rotationDirection)
        {
            float dot = Vector3.Dot(moveDirectionNormalized, rotationDirection);

            float normalizedDotValue = MathExtentions.Remap(dot, MIN_DOT, MAX_DOT, MIN_T, MAX_T);

            return Mathf.Lerp(_movementData.BackwardMoveSpeed, _movementData.ForwardMoveSpeed, normalizedDotValue) * _speedMultiplyer;
        }

        private float CurrentGravityForce()
        {
            if (_groundDetector.GroundDetected)
                _gravityForce = _movementData.GroundedGravityForce;
            else
                _gravityForce += _movementData.GravityForce * Time.deltaTime;

            return _gravityForce;
        }

        private void CalculateAngleDifferenec(Vector3 oldDirection, Vector3 newDirection)
        {
            float x1 = oldDirection.x;
            float y1 = oldDirection.z;

            float x2 = newDirection.x;
            float y2 = newDirection.z;

            _angleDifference = MathExtentions.FastAtan2(x1 * y2 - y1 * x2, x1 * x2 + y1 * y2) * Mathf.Rad2Deg;
        }

        private void CalculateTargetRotation(Vector3 direction)
        {
            _targetRotation = Quaternion.LookRotation(direction);
        }
    }
}