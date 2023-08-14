using Scripts.Data;
using Scripts.Infrastructure.Services;

namespace Scripts.Infrastructure
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}