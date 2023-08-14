using Scripts.Logic;
using UnityEngine;

namespace Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;
        private IHealth _health;

        private void Start()
        {
            _health = GetComponent<IHealth>();
            if (_health != null)
                _health.OnChangedHealth += UpdateHpBar;
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.OnChangedHealth -= UpdateHpBar;
        }

        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnChangedHealth += UpdateHpBar;
        }

        private void UpdateHpBar() =>
            HpBar.SetValue(_health.Current, _health.Max);
    }
}