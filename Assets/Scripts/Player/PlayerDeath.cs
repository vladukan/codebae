using System;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        public PlayerHealth PlayerHealth;
        public PlayerMove PlayerMove;
        public PlayerAttack PlayerAttack;
        public PlayerAnimator PlayerAnimator;
        public GameObject FxDeath;
        private bool _isDead = false;

        private void Start() =>
            PlayerHealth.OnChangedHealth += OnChangedHealth;

        private void OnDestroy() =>
            PlayerHealth.OnChangedHealth -= OnChangedHealth;

        private void OnChangedHealth()
        {
            if (!_isDead && PlayerHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            PlayerMove.enabled = false;
            PlayerAttack.enabled = false;
            PlayerAnimator.PlayDeath();
            Destroy(Instantiate(FxDeath, transform.position, Quaternion.identity), 1f);
        }
    }
}