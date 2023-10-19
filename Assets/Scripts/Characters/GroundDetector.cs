using UnityEngine;

namespace PesPatron.Characters
{
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField] private float _rayLenght = 0.5f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector3 _worldRayDirection = Vector3.down;

        private bool _groundDetected;

        public bool GroundDetected => _groundDetected;

        private void Update()
        {
            _groundDetected = Physics.Raycast(transform.position, _worldRayDirection, _rayLenght, _groundLayer);
        }
    }
}