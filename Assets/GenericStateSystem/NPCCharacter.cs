using System;
using UnityEngine;
using UnityEngine.AI;
namespace GenericStateSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCCharacter : BaseCharacter
    {
        public Transform[] wayPoints;
        public NPCProperties npcProperties;  // share all params to states
        [HideInInspector] public NavMeshAgent agent;
        public override void Start()
        {
            base.Start();
            agent = GetComponent<NavMeshAgent>();
           
        }

        public float TargetDistance(string _tag, out Vector3 _target)
        {
            RaycastHit hit;

            Vector3 p1 = transform.position + Vector3.up;
            
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