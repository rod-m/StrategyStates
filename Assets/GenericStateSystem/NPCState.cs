using UnityEngine;

namespace GenericStateSystem
{
    public abstract class NPCState : GenericState
    {
        protected NPCCharacter _character;
    
        public NPCState(NPCCharacter _c)
        {
            _character = _c;
            
        }
        
    }
}