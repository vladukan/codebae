using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public Attack Attack;
        public Follow Follow;
        public EnemyAnimator Animator;
        public GameObject FxDeath;
        public event Action OnDead;
        private bool _isDead;

        private void Start() =>
            Health.OnChangedHealth += OnChangedHealth;

        private void OnDestroy() =>
            Health.OnChangedHealth -= OnChangedHealth;

        private void OnChangedHealth()
        {
            if (!_isDead && Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            Health.OnChangedHealth -= OnChangedHealth;
            Animator.PlayDie();
            Attack.enabled = false;
            Follow.enabled = false;
            SpawnDeathFx();
            StartCoroutine(DestroyTimer());
            OnDead?.Invoke();
        }

        private void SpawnDeathFx() =>
            Destroy(Instantiate(FxDeath, transform.position, Quaternion.identity), 1f);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}