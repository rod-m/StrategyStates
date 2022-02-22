using GenericStateSystem;
using GenericStateSystem.ActionStates;
using UnityEngine;

namespace StrategyPattern
{
    public class EnemyStateMachine : NPCCharacter
    {
       
        public override void Start()
        {
            base.Start();
           
            defaultState = new PatrolState(this);
            stateMachine.MakeTransitionState(defaultState);
        }

    }
}