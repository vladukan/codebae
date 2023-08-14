using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        [Range(1, 100)] public int Hp;
        [Range(1f, 30f)] public float Damage;

        [FormerlySerializedAs("EffectiveDistance")] [Range(0.5f, 1f)]
        public float AttackEffectiveDistance;

        [FormerlySerializedAs("Cleavage")] [Range(0.5f, 1f)]
        public float AttackCleavage;

        [Range(1f, 20f)] public float MoveSpeed;
        public GameObject Prefab;
    }
}