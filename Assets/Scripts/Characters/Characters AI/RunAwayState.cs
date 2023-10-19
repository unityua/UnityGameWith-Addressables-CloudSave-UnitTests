using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Characters.AI
{
    public class RunAwayState : CharacterState
    {
        public override void StateEntered(SimpleCharacterAI characterAI)
        {

        }

        public override void StateUpdate(SimpleCharacterAI characterAI)
        {
            Vector3 moveDirection = CalculateRunAwayDirection(characterAI.Position, characterAI.NearEnemies);

            characterAI.Movement.SetMoveDirection(moveDirection);
        }

        public override void StateExited(SimpleCharacterAI characterAI)
        {

        }

        private Vector3 CalculateRunAwayDirection(Vector3 currentPosition, IReadOnlyList<Character> enemies)
        {
            Vector3 resultDirection = Vector3.zero;

            int enemiesCount = enemies.Count;

            for (int i = 0; i < enemiesCount; i++)
            {
                Vector3 directionFromEnemy = (currentPosition - enemies[i].Position).normalized;
                resultDirection += directionFromEnemy;
            }

            resultDirection /= enemiesCount;

            return resultDirection;
        }
    }
}