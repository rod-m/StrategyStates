using UnityEngine;

namespace GenericStateSystem.ActionStates
{
    public class PatrolState : NPCState
    {
        private int nextWaypoint = 0;
        public PatrolState(NPCCharacter _c) : base(_c) 
        {
        }

        public override void BeginState()
        {
            _character.anim.SetFloat("Speed", 0.5f); // walk
            if (_character.wayPoints != null)
            {
                ProceedToPoint();
            }
        }

        void ProceedToPoint()
        {
            int nextIndex = nextWaypoint % _character.wayPoints.Length;
            _character.agent.destination = _character.wayPoints[nextIndex].position;
            nextWaypoint++;
        }
        public override void UpdateState()
        {
            TransitionState();
        }

        public override void UpdatePhysicsState()
        {
            if (_character.agent.remainingDistance < 1f)
            {
                ProceedToPoint();
            }
        }

        public override void TransitionState()
        {

            Vector3 _target = Vector3.zero;
            if (_character.TargetDistance("Player", out _target) < _character.npcProperties.PlayerDistanceAlert)
            {
                _character.agent.destination = _target;
                _character.stateMachine.MakeTransitionState(new ChaseState(_character));
            }
        }

        public override void EndState()
        {
         
        }
    }
}