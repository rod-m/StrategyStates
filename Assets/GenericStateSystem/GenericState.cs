namespace GenericStateSystem
{
    public abstract class GenericState:IState
    {
        
        public abstract void BeginState();
        public abstract void UpdateState();
        public abstract void UpdatePhysicsState();
        public abstract void TransitionState();
        public abstract void EndState();
    }
}