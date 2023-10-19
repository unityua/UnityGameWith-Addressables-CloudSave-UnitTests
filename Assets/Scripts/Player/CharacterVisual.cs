using UnityEngine;

namespace PesPatron.Characters
{
    public class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator; 
    }
}