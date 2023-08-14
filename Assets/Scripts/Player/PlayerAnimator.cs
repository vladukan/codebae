using UnityEngine;

namespace Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        public Animator Animator;
        public CharacterController CharacterController;
        public bool IsAttacking { get; private set; }

        private void Update()
        {
            Animator.SetFloat(MoveHash, CharacterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        public void PlayAttack() =>
            Animator.SetTrigger(AttackHash);

        public void PlayHit() =>
            Animator.SetTrigger(HitHash);

        public void PlayDeath() =>
            Animator.SetTrigger(DieHash);

        private void OnAttackBegin() =>
            IsAttacking = true;

        private void OnAttackEnd() =>
            IsAttacking = false;
    }
}