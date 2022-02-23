using UnityEngine;
namespace GenericStateSystem.ActionStates
{
    public class MoveState : PlayerState
    {
        #region Variables

        private float _turnSpeedMultiplier;
        private float _speed = 0f;
        private float _direction = 0f;
        private bool _isSprinting = false;
       
        private Vector3 _targetDirection;
        private Vector2 _input;
        private Quaternion _freeRotation;
        private Camera _mainCamera;
        private float _velocity;
        #endregion Variables
        public MoveState(PlayerCharacter _c) : base(_c)
        {
        }

        public override void BeginState()
        {
            _mainCamera = Camera.main;
           
        }

        public override void UpdateState()
        {
            TransitionState();
            if (!_character.IsGrounded(_character.playerProperties))
            {
                return;
            }
            
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");
            // set speed to both vertical and horizontal inputs
            _speed = Mathf.Abs(_input.x) + Mathf.Abs(_input.y);
            _speed = Mathf.Clamp(_speed, 0f, 1f);
            _speed = Mathf.SmoothDamp(_character.anim.GetFloat("Speed"), _speed, ref _velocity, 0.1f);
            _character.anim.SetFloat("Speed", _speed);
            
            //is couching
            bool _isCrouching = (Input.GetKey(_character.playerProperties.CrouchKeyboard));
            // set sprinting
            _isSprinting = ((Input.GetKey(_character.playerProperties.SprintJoystick) || Input.GetKey(_character.playerProperties.SprintKeyboard)) && _input != Vector2.zero &&
                            _direction >= 0f);
            if (_isCrouching)
            {
                _isSprinting = false;
            }
            _character.anim.SetBool("isSprinting", _isSprinting);
            _character.anim.SetBool("isCrouching", _isCrouching);
            // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
            UpdateTargetDirection();
            if (_input != Vector2.zero && _targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = _targetDirection.normalized;
                _freeRotation = Quaternion.LookRotation(lookDirection, _character.transform.up);
                var diferenceRotation = _freeRotation.eulerAngles.y - _character.transform.eulerAngles.y;
                var eulerY = _character.transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = _freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, Quaternion.Euler(euler),
                    _character.playerProperties.TurnSpeed * _turnSpeedMultiplier * Time.deltaTime);
            }
        }

      
        private void UpdateTargetDirection()
        {
            _turnSpeedMultiplier = 1f;
            var forward = _mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = _mainCamera.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            _targetDirection = _input.x * right + _input.y * forward;
        }
        public override void UpdatePhysicsState()
        {
           
        }

        public override void TransitionState()
        {
            if (Input.GetKeyDown(_character.playerProperties.JumpKeyboard))
            {
                _character.stateMachine.MakeTransitionState(new JumpState(_character));
            }
        }

        public override void EndState()
        {
            _character.anim.SetBool("isSprinting", false);
            _character.anim.SetBool("isCrouching", false);
        }
        
    }
}