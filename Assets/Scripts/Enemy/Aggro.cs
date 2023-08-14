using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver Trigger;
        public Follow Follow;
        public float CoolDown = 1f;
        private bool _hasAggroTarget = false;
        private Coroutine _aggroCoroutine;

        private void Start()
        {
            Trigger.TriggerEnter += TriggerEnter;
            Trigger.TriggerExit += TriggerExit;
            FollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
                FollowOn();
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
                _aggroCoroutine = StartCoroutine(SwitchAggroDelay());
        }

        private IEnumerator SwitchAggroDelay()
        {
            yield return new WaitForSeconds(CoolDown);
            _hasAggroTarget = false;
            FollowOff();
        }

        private void FollowOff() =>
            Follow.enabled = false;

        private void FollowOn()
        {
            _hasAggroTarget = true;
            if (_aggroCoroutine != null)
                StopCoroutine(_aggroCoroutine);
            Follow.enabled = true;
        }
    }
}