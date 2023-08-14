using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public NavMeshAgent Agent;
        private Transform _player;

        private const float MinDistance = 1f;

        public void Construct(Transform player) =>
            _player = player;

        private void Update()
        {
            if (_player && PlayerNotReached())
                Agent.destination = _player.position;
        }

        private bool PlayerNotReached() =>
            Vector3.Distance(Agent.transform.position, _player.position) >= MinDistance;
    }
}