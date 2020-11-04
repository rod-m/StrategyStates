using UnityEngine;
namespace GenericStateSystem.ActionStates
{
    public class MoveState : GenericState
    {
        public bool UseCharacterForward = true;
        public bool LockToCameraForward = false;
        public float TurnSpeed = 10f;
        public KeyCode SprintJoystick = KeyCode.JoystickButton2;
        public KeyCode SprintKeyboard = KeyCode.F;
        public KeyCode CrouchKeyboard = KeyCode.LeftShift;
        public KeyCode JumpKeyboard = KeyCode.Space;
        private float _turnSpeedMultiplier;
        private float _speed = 0f;
        private float _direction = 0f;
        private bool _isSprinting = false;
        private Animator _anim;
        private Vector3 _targetDirection;
        private Vector2 _input;
        private Quaternion _freeRotation;
        private Camera _mainCamera;
        private float _velocity;
        public MoveState(BaseCharacter _c, GenericStateMachine _s) : base(_c, _s)
        {
        }

        public override void BeginState()
        {
            _mainCamera = Camera.main;
            _anim = _character.anim;
        }

        public override void UpdatesTATE()
        {
            TransitionState();
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");
        }
        

        public override void UpdatePhysicsState()
        {
            // set speed to both vertical and horizontal inputs
            if (_character.useCharacterForward)
                _speed = Mathf.Abs(_input.x) + _input.y;
            else
                _speed = Mathf.Abs(_input.x) + Mathf.Abs(_input.y);

            _speed = Mathf.Clamp(_speed, 0f, 1f);
            _speed = Mathf.SmoothDamp(_character.anim.GetFloat("Speed"), _speed, ref _velocity, 0.1f);
            _character.anim.SetFloat("Speed", _speed);

            if (_input.y < 0f && _character.useCharacterForward)
                _direction = _input.y;
            else
                _direction = 0f;

            _character.anim.SetFloat("Direction", _direction);
            //is couching
            bool _isCrouching = (Input.GetKey(CrouchKeyboard));
            // set sprinting
            _isSprinting = ((Input.GetKey(SprintJoystick) || Input.GetKey(SprintKeyboard)) && _input != Vector2.zero &&
                            _direction >= 0f);
            if (_isCrouching)
            {
                _isSprinting = false;
            }
            _anim.SetBool("isSprinting", _isSprinting);
            _anim.SetBool("isCrouching", _isCrouching);
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
                    TurnSpeed * _turnSpeedMultiplier * Time.deltaTime);
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

        public override void TransitionState()
        {
            if (Input.GetKeyDown(JumpKeyboard))
            {
                _character.stateMachine.MakeTransitionState(new JumpState(_character, _character.stateMachine));
            }
        }

        public override void EndState()
        {
            _anim.SetBool("isSprinting", false);
            _anim.SetBool("isCrouching", false);
        }
    }
}