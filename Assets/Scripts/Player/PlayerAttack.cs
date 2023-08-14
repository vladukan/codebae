using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.Input;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(PlayerAnimator), typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        public PlayerAnimator PlayerAnimator;
        public CharacterController CharacterController;
        private IInputService _input;
        private static LayerMask _mask;
        private Collider[] _hits = new Collider[3];
        private PlayerStats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _mask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !PlayerAnimator.IsAttacking)
            {
                PlayerAnimator.PlayAttack();
            }
        }

        public void LoadProgress(PlayerProgress progress) =>
            _stats = progress.PlayerStats;

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint(), _stats.DamageRadius, _hits, _mask);
        }

        private Vector3 StartPoint() =>
            transform.position + transform.forward;
    }
}