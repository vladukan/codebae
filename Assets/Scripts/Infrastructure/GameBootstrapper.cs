using Scripts.Infrastructure;
using Scripts.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}