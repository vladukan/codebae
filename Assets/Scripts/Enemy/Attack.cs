using System.Linq;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator Animator;
        public float AttackCoolDown = 3f;
        public float Cleavage = 0.5f;
        public float Damage = 10f;
        public float EffectiveDistance = 0.5f;

        private Transform _player;
        private float _attackCoolDown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        public void Construct(Transform playerTransform) =>
            _player = playerTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (!CoolDownIsUp())
                _attackCoolDown -= Time.deltaTime;
            if (CanAttack())
                StartAttack();
        }

        public void DisableAttack() =>
            _attackIsActive = false;

        public void EnableAttack() =>
            _attackIsActive = true;

        private void OnAttackBegin()
        {
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1f);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint() =>
            transform.position + transform.up + transform.forward * EffectiveDistance;

        private void OnAttackEnd()
        {
            _attackCoolDown = AttackCoolDown;
            _isAttacking = false;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CoolDownIsUp();

        private bool CoolDownIsUp() =>
            _attackCoolDown <= 0f;

        private void StartAttack()
        {
            transform.LookAt(_player);
            Animator.PlayAttack();
            _isAttacking = true;
        }
    }
}