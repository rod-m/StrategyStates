using System;
using GenericStateSystem.ActionStates;
using UnityEngine;

namespace GenericStateSystem
{
   [RequireComponent(typeof(Animator))]
   [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        #region Variables

        public bool useCharacterForward = false;
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody rb;
        public float collissionOverLapRadius = 0.1f;
        public LayerMask whatIsGround;
        #endregion Variables

        #region States

        public IState defaultState;
        

        #endregion States

        #region StateMachineVariables

        public GenericStateMachine stateMachine;
        

        #endregion StateMachineVariables

        #region Animation

        public bool IsGrounded()
        {
            return Physics.OverlapSphere(transform.position,
                collissionOverLapRadius, whatIsGround).Length > 0;
        }
        

        #endregion

        #region MonoBehaviourCallbacks

        public virtual void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            stateMachine = new GenericStateMachine();
            defaultState = new DefaultState(this, stateMachine);
            stateMachine.InitState(defaultState);
        }

        // Update is called once per frame
        public virtual void Update()
        {
            stateMachine.ActiveState.UpdatesTATE();
            
        }

        public virtual void FixedUpdate()
        {
            stateMachine.ActiveState.UpdatePhysicsState();
        }

        #endregion MonoBehaviourCallbacks
   
    }
}
