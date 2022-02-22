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

        //public bool useCharacterForward = false;
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody rb;
        //public float collissionOverLapRadius = 0.1f;
        //public LayerMask whatIsGround;
        #endregion Variables

        #region States

        public IState defaultState;
        

        #endregion States

        #region StateMachineVariables

        public GenericStateMachine stateMachine;
        

        #endregion StateMachineVariables

        #region Animation

      
        

        #endregion

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
        public bool IsGrounded(IGrounded _prop)
        {
            return Physics.OverlapSphere(transform.position,
                _prop.collissionOverLapRadius, _prop.whatIsGround).Length > 0;
        }
    }
}
