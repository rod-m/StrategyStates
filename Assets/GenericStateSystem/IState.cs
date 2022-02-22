namespace GenericStateSystem
{
    public interface IState
    {
        void BeginState();
        void UpdateState();
        void UpdatePhysicsState();
        void TransitionState();
        void EndState();
    }
}