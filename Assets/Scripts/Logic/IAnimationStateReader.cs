namespace Scripts.Logic
{
    public interface IAnimationStateReader
    {
        void EnteredState(int hash);
        void ExitedState(int hash);
        AnimatorState State { get; }
    }
}