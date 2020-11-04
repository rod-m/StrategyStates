namespace GenericStateSystem
{
    public abstract class GenericState:IState
    {
        protected BaseCharacter _character;
        protected GenericStateMachine _stateMachine;

        public GenericState(BaseCharacter _c, GenericStateMachine _s)
        {
            _character = _c;
            _stateMachine = _s;
        }
        public abstract void BeginState();
        public abstract void UpdatesTATE();
        public abstract void UpdatePhysicsState();
        public abstract void TransitionState();
        public abstract void EndState();
    }
}