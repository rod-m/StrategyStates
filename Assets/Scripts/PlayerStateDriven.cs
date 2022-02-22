using System.Collections;
using System.Collections.Generic;
using GenericStateSystem;
using GenericStateSystem.ActionStates;
using UnityEngine;

public class PlayerStateDriven : BaseCharacter
{
    void Start()
    {
        base.Start();
        defaultState = new MoveState(this, stateMachine);
        stateMachine.MakeTransitionState(defaultState);
    }


}
