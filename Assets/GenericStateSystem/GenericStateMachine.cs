using UnityEngine;

namespace GenericStateSystem
{
    public class GenericStateMachine
    {
        public GenericState ActiveState { get; private set; }
        public void InitState(GenericState beginState)
        {
            Debug.Log($"InitState {beginState.GetType().Name}");
            ActiveState = beginState;
            beginState.BeginState();
        }

        public void MakeTransitionState(GenericState nextState)
        {
            Debug.Log($"MakeTransitionState {nextState.GetType().Name}");
            ActiveState.EndState();
            ActiveState = nextState;
            nextState.BeginState();
        }
    }
}