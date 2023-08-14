using UnityEngine;

namespace Scripts.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Attack = "Attack";
        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Attack);
        }

        protected static Vector2 SimpleAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }

        protected static Vector2 UnityAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        }
    }
}