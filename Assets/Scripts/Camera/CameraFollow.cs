using UnityEngine;

namespace PesPatron.CameraStuff
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [Space]
        [SerializeField] private Vector3 _possitionOffset;
        [SerializeField] private Vector3 _rotationOffset;
        [SerializeField] private bool _lookAt;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private Transform _target;

        public Transform Target => _target;
        public Camera MainCamera => _mainCamera;

        private void LateUpdate()
        {
            if (_target == null)
                return;

            float deltaTime = Time.deltaTime;

            Vector3 desiredPosition = _target.position + _possitionOffset;
            Quaternion desiredRotation = Quaternion.Euler(_rotationOffset);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, _moveSpeed * deltaTime);

            Quaternion targetRotation;

            if (_lookAt)
                targetRotation = Quaternion.LookRotation(_target.position - transform.position) * desiredRotation;
            else
                targetRotation = desiredRotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * deltaTime);
        }

        public void SetTraget(Transform target, bool jumpImmidiate = false)
        {
            _target = target;

            if (jumpImmidiate)
                ResetPosition();
        }

        public void ResetPosition()
        {
            Vector3 desiredPosition = _target.position + _possitionOffset;
            transform.position = desiredPosition;
            transform.rotation = Quaternion.LookRotation(_target.position - transform.position) * Quaternion.Euler(_rotationOffset);
        }
    }
}