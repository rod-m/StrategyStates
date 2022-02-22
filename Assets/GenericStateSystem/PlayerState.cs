using UnityEngine;

namespace GenericStateSystem
{
    public abstract class PlayerState : GenericState
    {
        protected PlayerCharacter _character;
    
        public PlayerState(PlayerCharacter _c)
        {
            _character = _c;
            
        }
   
       
    }
}