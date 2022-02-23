using System.Collections;
using System.Collections.Generic;
using GenericStateSystem;
using GenericStateSystem.ActionStates;
using UnityEngine;

namespace StrategyPattern
{
    public class PlayerStateDriven : PlayerCharacter
    {
        public override void Start()
        {
            base.Start();
            defaultState = new MoveState(this);
            stateMachine.MakeTransitionState(defaultState);
        }


    }
}