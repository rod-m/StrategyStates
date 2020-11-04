namespace GenericStateSystem
{
    public interface IState
    {
        void BeginState();
        void UpdatesTATE();
        void UpdatePhysicsState();
        void TransitionState();
        void EndState();
    }
}