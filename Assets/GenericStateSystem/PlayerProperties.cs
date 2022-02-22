using UnityEngine;

namespace GenericStateSystem
{
    [CreateAssetMenu(fileName = "PlayerParams", menuName = "State Machine/Player Properties", order = 0)]
    public class PlayerProperties : ScriptableObject, IGrounded
    {
        [SerializeField]private LayerMask _whatIsGround;
        [SerializeField]private float _collissionOverLapRadius = 0.1f;
       
        public float collissionOverLapRadius { 
            get { return _collissionOverLapRadius;}
            set { _collissionOverLapRadius = value; } 
        }
        public LayerMask whatIsGround
        {
            get { return _whatIsGround;}
            set { _whatIsGround = value; }
        }
        public bool useCharacterForward = false;
       
        public float JumpForce = 10f;
        public float TurnSpeed = 10f;
        public KeyCode SprintJoystick = KeyCode.JoystickButton2;
        public KeyCode SprintKeyboard = KeyCode.F;
        public KeyCode CrouchKeyboard = KeyCode.LeftShift;
        public KeyCode JumpKeyboard = KeyCode.Space;
    }
}