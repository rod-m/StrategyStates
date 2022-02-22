using UnityEngine;

namespace GenericStateSystem.ActionStates
{
    public class JumpState:GenericState
    {
        private Animator _anim;
        public float JumpForce = 10f;
        private int _startJump = 0;
        public JumpState(BaseCharacter _c, GenericStateMachine _s) : base(_c, _s)
        {
        }

        public override void BeginState()
        {
            _startJump = 0;
            _anim = _character.anim;
            _anim.SetTrigger("Jump");
            if (_character.IsGrounded())
            {
                _character.rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
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
            if (_character.IsGrounded() && _startJump > 90)
            {
                _character.stateMachine.MakeTransitionState(new MoveState(_character, _character.stateMachine));
            }

            _startJump++;
        }

        public override void EndState()
        {
            
        }
    }
}