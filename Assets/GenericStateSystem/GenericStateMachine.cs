using UnityEngine;

namespace GenericStateSystem
{
    public class GenericStateMachine
    {
        public IState ActiveState { get; private set; }
        public void InitState(IState beginState)
        {
            Debug.Log($"InitState {beginState.GetType().Name}");
            ActiveState = beginState;
            beginState.BeginState();
        }

        public void MakeTransitionState(IState nextState)
        {
            Debug.Log($"MakeTrans {nextState.GetType().Name}");
            ActiveState.EndState();
            ActiveState = nextState;
            nextState.BeginState();
        }
    }
}