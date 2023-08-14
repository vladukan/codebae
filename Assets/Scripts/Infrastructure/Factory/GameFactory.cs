using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Scripts.Enemy;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using Scripts.StaticData;
using Scripts.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        private GameObject _player { get; set; }

        public GameObject CreateHud(LoadLevelState loadLevelState)
        {
            return InstantiateRegistered(AssetPath.HudPath);
        }

        public GameObject CreatePlayer(GameObject at)
        {
            _player = InstantiateRegistered(AssetPath.PlayerPath, at.transform.position);
            return _player;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);
            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;
            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_player.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            var attack = monster.GetComponent<Attack>();
            attack.Construct(_player.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.AttackCleavage;
            attack.EffectiveDistance = monsterData.AttackEffectiveDistance;

            return monster;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void Register(ISavedProgressReader reader)
        {
            if (reader is ISavedProgress writer)
                ProgressWriters.Add(writer);
            ProgressReaders.Add(reader);
        }

        private GameObject InstantiateRegistered(string path, Vector3 at)
        {
            GameObject player = _assets.Instantiate(path, at);
            RegisterProgressWatchers(player);
            return player;
        }

        private GameObject InstantiateRegistered(string path)
        {
            GameObject player = _assets.Instantiate(path);
            RegisterProgressWatchers(player);
            return player;
        }

        private void RegisterProgressWatchers(GameObject player)
        {
            foreach (ISavedProgressReader reader in player.GetComponentsInChildren<ISavedProgressReader>())
                Register(reader);
        }
    }
}