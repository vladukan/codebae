using UnityEngine;

namespace Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public Vector2 Axis { get; }
        public bool IsAttackButtonUp();
    }
}