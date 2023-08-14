namespace Scripts.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedSate<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
}