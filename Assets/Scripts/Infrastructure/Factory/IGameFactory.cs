using System;
using System.Collections.Generic;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHud(LoadLevelState loadLevelState);
        GameObject CreatePlayer(GameObject at);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        void Register(ISavedProgressReader reader);
        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
    }
}