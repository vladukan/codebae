using System;
using System.Collections.Generic;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;

namespace Scripts.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] =
                    new LoadLevelState(
                        stateMachine: this,
                        sceneLoader: sceneLoader,
                        curtain: curtain,
                        gameFactory: services.Single<IGameFactory>(),
                        progressService: services.Single<IPersistentProgressService>()),
                [typeof(LoadProgressState)] =
                    new LoadProgressState(
                        this,
                        services.Single<IPersistentProgressService>(),
                        services.Single<ISaveLoadService>()
                    ),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }


        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedSate<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}