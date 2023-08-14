using Scripts.CameraLogic;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using Scripts.Player;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadedSate<string>
    {
        private const string InitialPoint = "PlayerInitialPoint";
        private const string EnemySpawnerTag = "EnemySpawner";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string name)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(name, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitSpawners();
            GameObject player = InitPlayer();
            InitHud(player);
            CameraFollow(player);
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitSpawners()
        {
            foreach (GameObject spawner in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                EnemySpawner enemySpawner = spawner.GetComponent<EnemySpawner>();
                _gameFactory.Register(enemySpawner);
            }
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_progressService.Progress);
        }

        private GameObject InitPlayer()
        {
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPoint));
            return player;
        }

        private void InitHud(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHud(this);
            hud.GetComponentInChildren<ActorUI>().Construct(player.GetComponent<IHealth>());
            hud.GetComponentInChildren<AttackUI>().Construct(player.GetComponent<PlayerAnimator>());
        }

        private static void CameraFollow(GameObject player) =>
            Camera.main.GetComponent<CameraFollow>().Follow(player);
    }
}