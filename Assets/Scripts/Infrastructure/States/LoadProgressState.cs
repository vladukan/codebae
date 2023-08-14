using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;

namespace Scripts.Infrastructure
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress("MainScene");
            progress.PlayerState.MaxHP = 50f;
            progress.PlayerStats.Damage = 3f;
            progress.PlayerStats.DamageRadius = 0.5f;
            progress.PlayerState.ResetHP();
            return progress;
        }
    }
}