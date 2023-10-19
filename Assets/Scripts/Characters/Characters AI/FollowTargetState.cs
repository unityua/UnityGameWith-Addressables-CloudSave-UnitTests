using PesPatron.Helpers;
using UnityEngine;

namespace PesPatron.Characters.AI
{
    public class FollowTargetState : CharacterState
    {
        public override void StateEntered(SimpleCharacterAI characterAI)
        {
            
        }

        public override void StateUpdate(SimpleCharacterAI characterAI)
        {
            Character target = characterAI.NearFollowTargets[0];

            Vector3 targetPosition = target.Position;
            Vector3 currentPosition = characterAI.Position;

            if (Vector3.Distance(currentPosition, targetPosition) > characterAI.DistanceToStopFollow)
            {
                Vector3 moveDirection = (targetPosition - currentPosition).WithY(0f).normalized;
                characterAI.Movement.SetMoveDirection(moveDirection);
            }
        }

        public override void StateExited(SimpleCharacterAI characterAI)
        {
            
        }
    }
}