using System;
using UnityEngine;
using UnityEngine.AI;
namespace GenericStateSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(LookAt))]
    public class NPCCharacter : BaseCharacter
    {
        Vector2 smoothDeltaPosition = Vector2.zero;
        Vector2 velocity = Vector2.zero;
        [HideInInspector] public LookAt lookAt;
        public Transform[] wayPoints;
        public NPCProperties npcProperties;  // share all params to states
        [HideInInspector] public NavMeshAgent agent;
        
        private float _turnSpeedMultiplier;
        private float _speed = 0f;
        private float _direction = 0f;
        private bool _isSprinting = false;
       
        private Vector3 _targetDirection;
      
        private Quaternion _freeRotation;
        private Camera _mainCamera;
        private float _velocity;
        public override void Start()
        {
            base.Start();
            _mainCamera = Camera.main;
            lookAt = GetComponent<LookAt>();
            agent = GetComponent<NavMeshAgent>();
            agent.updatePosition = true;
        }

     
    
        public float TargetDistance(string _tag, out Vector3 _target)
        {
            RaycastHit hit;

            Vector3 p1 = transform.position + Vector3.up;
            Debug.DrawLine(p1, transform.forward * npcProperties.PlayerDistanceAlert, Color.blue);

            // Cast a sphere wrapping character controller 10 meters forward
            // to see if it is about to hit anything.
            if (Physics.SphereCast(p1, npcProperties.height / 2, transform.forward, out hit, npcProperties.PlayerDistanceAlert))
            {
                
                if (hit.collider.CompareTag(_tag))
                { 
                    Debug.Log($"{_tag} Spotted in Range");
                    //agent.destination = hit.transform.position;
                    _target = hit.transform.position;
                    return hit.distance;
                    //stateMachine.MakeTransitionState(new ChaseState(_character));
                }
            }
            _target = Vector3.zero;
            return Single.PositiveInfinity;
        }
    }
}