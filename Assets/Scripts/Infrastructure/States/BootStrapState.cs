using Infrastructure.Services;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.Input;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class BootStrapState : IState
    {
        private const string InitialScene = "InitialScene";
        private const string Mainscene = "MainScene";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAssets>(), _services.Single<IStaticDataService>()));
            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle(staticData);
        }

        public void Exit()
        {
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}