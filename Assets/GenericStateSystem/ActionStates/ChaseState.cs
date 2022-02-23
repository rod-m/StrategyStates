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
         //   _character.anim.speed = 2f;
            _character.agent.speed = _character.npcProperties.ChaseSpeed;
            _character.anim.SetFloat("Speed", 1.0f);
        }

        public override void UpdateState()
        {
            Debug.DrawRay(_character.transform.position, Vector3.forward, Color.green);
            TransitionState();
        }

        public override void UpdatePhysicsState()
        {
            // find player current location
            Vector3 _target = Vector3.zero;
            if (_character.TargetDistance("Player", out _target) < _character.npcProperties.PlayerDistanceAlert)
            {
                _character.agent.destination = _target;
               
            }
      
        }

        public override void TransitionState()
        {
            float durationOfChase = Time.time - startedChase;
            Debug.Log($"durationOfChase {durationOfChase}");
            if ( durationOfChase > _character.npcProperties.ChaseTime)
            {
                _character.stateMachine.MakeTransitionState(new PatrolState(_character));
            }
           else if (_character.agent.remainingDistance < _character.npcProperties.AttackDistance)
            {
                _character.stateMachine.MakeTransitionState(new AttackState(_character));
            }
        }

        public override void EndState()
        {
           
        }
    }
}