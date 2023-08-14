﻿using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinVelocity = 0.05f;
        public NavMeshAgent Agent;
        public EnemyAnimator Animator;

        private void Update()
        {
            if (ShouldMove())
                Animator.Move(Agent.velocity.magnitude);
            else
                Animator.StopMoving();
        }

        private bool ShouldMove() =>
            Agent.velocity.magnitude > MinVelocity && Agent.remainingDistance > Agent.radius;
    }
}