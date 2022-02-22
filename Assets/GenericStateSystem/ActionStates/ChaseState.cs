using UnityEngine;

namespace GenericStateSystem.ActionStates
{
    public class ChaseState : NPCState
    {
        private float startedChase = 0f;
        public ChaseState(NPCCharacter _c) : base(_c)
        {
        }
        public override void BeginState()
        {
            startedChase = Time.time;
            _character.anim.SetFloat("Speed", 1f);
        }

        public override void UpdateState()
        {
   
        }

        public override void UpdatePhysicsState()
        {
            // find player current location
            Vector3 _target = Vector3.zero;
            if (_character.TargetDistance("Player", out _target) < _character.npcProperties.PlayerDistanceAlert)
            {
                _character.agent.destination = _target;
               
            }
         TransitionState();
        }

        public override void TransitionState()
        {
            if (Time.time - startedChase > _character.npcProperties.ChaseTime)
            {
                _character.stateMachine.MakeTransitionState(new PatrolState(_character));
            }
          
        }

        public override void EndState()
        {
           
        }
    }
}