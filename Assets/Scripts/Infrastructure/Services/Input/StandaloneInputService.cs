using UnityEngine;

namespace Scripts.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                var axis = SimpleAxis();
                if (axis == Vector2.zero) axis = UnityAxis();
                return axis;
            }
        }
    }
}