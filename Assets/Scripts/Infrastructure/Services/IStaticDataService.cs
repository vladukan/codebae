using Scripts.Infrastructure.Services;
using Scripts.StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
    }
}