using UnityEngine;

namespace GenericStateSystem
{
    [CreateAssetMenu(fileName = "NPCProperties", menuName = "State Machine/NPC Properties", order = 0)]
    public class NPCProperties : ScriptableObject, IGrounded
    {
        public float PlayerDistanceAlert = 10f;
        public float AttackDistance = 1f;
        public float AttackSpeed = 3f;
        public float PatrolSpeed = 1f;
        public float ChaseSpeed = 3f;
        public float ChaseTime = 10f;
        public float TurnSpeed = 10f;
        public float height = 2f;

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
    }
}