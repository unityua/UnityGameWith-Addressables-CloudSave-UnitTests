using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PesPatron.Characters.AI
{
    public class SimpleCharacterAI : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private CharactersTrigger _characterTrigger;
        [Space]
        [SerializeField] private float _distanceToStopFollow = 1f;
        [SerializeField] private CharacterType[] _followTypes = System.Array.Empty<CharacterType>();
        [SerializeField] private CharacterType[] _enemyTypes = System.Array.Empty<CharacterType>();

        private CharacterState _currentState;
        
        private IdleCharacterState _idleState = new IdleCharacterState();
        private FollowTargetState _followTargetState = new FollowTargetState();
        private RunAwayState _runAwayState = new RunAwayState();

        private List<Character> _nearFollowTargets = new List<Character>();
        private List<Character> _nearEnemies = new List<Character>();

        private bool _initialized;

        public IReadOnlyList<Character> NearFollowTargets => _nearFollowTargets;
        public IReadOnlyList<Character> NearEnemies => _nearEnemies;

        public Vector3 Position => _character.Position;
        public CharacterMovement Movement => _character.Movement;

        public float DistanceToStopFollow  => _distanceToStopFollow; 

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            _currentState.StateUpdate(this);
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            SetState(_idleState);

            _characterTrigger.CharacterEntered += CharacterEntered;
            _characterTrigger.CharacterExited += CharacterExited;
        }

        private void TryChangeState()
        {
            if (_nearEnemies.Count > 0)
                SetState(_runAwayState);
            else if (_nearFollowTargets.Count > 0)
                SetState(_followTargetState);
            else
                SetState(_idleState);
        }

        private void SetState(CharacterState newState)
        {
            if (_currentState != null)
                _currentState.StateExited(this);

            _currentState = newState;
            newState.StateEntered(this);
        }

        private void CharacterEntered(Character enteredCharacter)
        {
            if (enteredCharacter == _character)
                return;

            if(_followTypes.Contains(enteredCharacter.CharacterType))
                _nearFollowTargets.Add(enteredCharacter);
            if(_enemyTypes.Contains(enteredCharacter.CharacterType))
                _nearEnemies.Add(enteredCharacter);

            TryChangeState();
        }

        private void CharacterExited(Character exitedCharacter)
        {
            if (exitedCharacter == _character)
                return;

            _nearFollowTargets.Remove(exitedCharacter);
            _nearEnemies.Remove(exitedCharacter);

            TryChangeState();
        }
    }
}