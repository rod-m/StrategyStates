using UnityEngine;

namespace GenericStateSystem.ActionStates
{
    public class JumpState:PlayerState
    {

        public JumpState(PlayerCharacter _c) : base(_c) 
        {
        }

        public override void BeginState()
        {
            if (_character.IsGrounded(_character.playerProperties))
            {
                _character.anim.SetFloat("Speed", 0); // stop wall anim
                _character.anim.SetTrigger("Jump");
                _character.anim.applyRootMotion = false; // so jump move forward
                _character.rb.AddForce(Vector3.up * _character.playerProperties.JumpForce, ForceMode.Impulse);
            }
        }

        public override void UpdateState()
        {
            TransitionState();
            
        }

        public override void UpdatePhysicsState()
        {
        
        }

        public override void TransitionState()
        {
            if (_character.IsGrounded(_character.playerProperties))
            {
                
                _character.stateMachine.MakeTransitionState(new MoveState(_character));
            }
        
        }

        public override void EndState()
        {
            _character.anim.applyRootMotion = true;
        }
    }
}