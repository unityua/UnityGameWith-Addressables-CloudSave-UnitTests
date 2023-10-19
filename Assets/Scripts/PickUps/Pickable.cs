using UnityEngine;

namespace PesPatron.Pickables
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private bool _flipOnPickup;
        [SerializeField] private Vector3 _pickedUpLocalRotation = Vector3.zero;

        public float Height => _height;
        public bool FlipOnPickup => _flipOnPickup;
        public Vector3 PickedUpLocalRotation => _pickedUpLocalRotation;
    }
}