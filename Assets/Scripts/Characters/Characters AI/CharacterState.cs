using UnityEngine;

namespace PesPatron.Characters.AI
{
    public abstract class CharacterState 
    {
        public abstract void StateEntered(SimpleCharacterAI characterAI);
        public abstract void StateUpdate(SimpleCharacterAI characterAI);
        public abstract void StateExited(SimpleCharacterAI characterAI);
    }
}