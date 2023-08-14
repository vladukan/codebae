using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class AttackUI : MonoBehaviour
    {
        public Button BtnAttack;
        private PlayerAnimator _animator;

        public void Construct(PlayerAnimator animator)
        {
            _animator = animator;
            BtnAttack.onClick.AddListener(_animator.PlayAttack);
        }

        private void OnDestroy()
        {
            BtnAttack.onClick.RemoveListener(_animator.PlayAttack);
        }
    }
}