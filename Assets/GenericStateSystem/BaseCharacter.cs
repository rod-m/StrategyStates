using System;
using GenericStateSystem.ActionStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace GenericStateSystem
{
   [RequireComponent(typeof(Animator))]
   [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        #region Variables
        
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody rb;
  
        #endregion Variables

        public GenericState defaultState;
    
        public GenericStateMachine stateMachine;
        
        #region MonoBehaviourCallbacks

        public virtual void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            stateMachine = new GenericStateMachine();
            defaultState = new DefaultState();
            stateMachine.InitState(defaultState);
        }
        
        public virtual void Update()
        {
            stateMachine.ActiveState.UpdateState();
            
        }

        public virtual void FixedUpdate()
        {
            stateMachine.ActiveState.UpdatePhysicsState();
        }

        #endregion MonoBehaviourCallbacks
        
        // use IGrounded Interface for shared properties between Player and NPC
        public bool IsGrounded(IGrounded _prop)
        {
            return Physics.OverlapSphere(transform.position,
                _prop.collissionOverLapRadius, _prop.whatIsGround).Length > 0;
        }
    }
}
