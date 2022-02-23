using UnityEngine;

namespace GenericStateSystem.ActionStates
{
    public class AttackState : NPCState
    {
        private float _attackTime = 0f;
        public AttackState(NPCCharacter _c) : base(_c)
        {
        }

        public override void BeginState()
        {
            _character.anim.SetFloat("Speed", 0f); // walk
            // set start of attack
          //  _character.agent.updatePosition = false;
            _character.agent.speed = 0;
            _attackTime = Time.time;
        
            _character.anim.SetTrigger("AttackRight");
        }

        public override void UpdateState()
        {
            Debug.DrawRay(_character.transform.position, Vector3.forward, Color.red);
            TransitionState();
        }

        public override void UpdatePhysicsState()
        {
          
          
        }

        public override void TransitionState()
        {
            float lastAttackTime = Time.time - _attackTime;
            if (lastAttackTime > _character.npcProperties.AttackSpeed)
            {
                _character.stateMachine.MakeTransitionState(new PatrolState(_character));
            }
        }

        public override void EndState()
        {
            _character.agent.speed = 1f;
            _character.agent.updatePosition = true;
        }
    }
}