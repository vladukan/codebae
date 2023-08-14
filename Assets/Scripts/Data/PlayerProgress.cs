using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerState PlayerState;
        public WorldData WorldData;
        public PlayerStats PlayerStats;
        public KillData KillData;


        public PlayerProgress(string initialLevel)
        {
            PlayerState = new PlayerState();
            WorldData = new WorldData(initialLevel);
            PlayerStats = new PlayerStats();
            KillData = new KillData();
        }
    }
}