using System;

namespace Scripts.Logic
{
    public interface IHealth
    {
        event Action OnChangedHealth;
        float Current { get; set; }
        float Max { get; set; }
        void TakeDamage(float damage);
    }
}