using System;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _hitStateHash = Animator.StringToHash("GetHit");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _moveStateHash = Animator.StringToHash("Move");
        private readonly int _dieStateHash = Animator.StringToHash("Die");

        private Animator _animator;
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        public AnimatorState State { get; private set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayHit() =>
            _animator.SetTrigger(Hit);

        public void PlayDie() =>
            _animator.SetTrigger(Die);

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving() =>
            _animator.SetBool(IsMoving, false);

        public void PlayAttack() =>
            _animator.SetTrigger(Attack);

        public void EnteredState(int hash)
        {
            State = StateFor(hash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int hash) =>
            StateExited?.Invoke(State);

        private AnimatorState StateFor(int hash)
        {
            AnimatorState state;
            if (hash == _hitStateHash) state = AnimatorState.Hit;
            else if (hash == _moveStateHash) state = AnimatorState.Move;
            else if (hash == _dieStateHash) state = AnimatorState.Die;
            else if (hash == _attackStateHash) state = AnimatorState.Attack;
            else if (hash == _attackStateHash) state = AnimatorState.Idle;
            else state = AnimatorState.Unknown;
            return state;
        }
    }
}