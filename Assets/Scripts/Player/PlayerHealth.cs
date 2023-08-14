using System;
using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public PlayerAnimator Animator;
        private PlayerState _playerState;

        public event Action OnChangedHealth;

        public float Current
        {
            get => _playerState.CurrentHP;
            set
            {
                if (_playerState.CurrentHP != value)
                {
                    _playerState.CurrentHP = value;
                    OnChangedHealth?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _playerState.MaxHP;
            set => _playerState.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerState = progress.PlayerState;
            OnChangedHealth?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHP = Current;
            progress.PlayerState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            Animator.PlayHit();
            Current -= damage;
        }
    }
}