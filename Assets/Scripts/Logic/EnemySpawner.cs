using System;
using Scripts.Data;
using Scripts.Enemy;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        private string _id;
        private IGameFactory _factory;
        [SerializeField] private bool _slain;
        private EnemyDeath _enemyDeath;
        public bool Slain => _slain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.OnDead += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.OnDead -= Slay;
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}