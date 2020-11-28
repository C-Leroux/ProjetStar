using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data", order = 51)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private string enemyName;
        [SerializeField]
        private Sprite sprite;
        [SerializeField]
        private float maxHP;
        [SerializeField]
        private int speed;
        [SerializeField]
        private int atkDamage;
        [SerializeField]
        private int atkRange;
        [SerializeField]
        private int droppedMoney;

        public string EnemyName
        {
            get
            {
                return enemyName;
            }
        }

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }
        }

        public float MaxHP
        {
            get
                {
                return maxHP;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
        }

        public int AtkDamage
        {
            get
            {
                return atkDamage;
            }
        }

        public int AtkRange
        {
            get
            {
                return atkRange;
            }
        }
        
        public int DroppedMoney
        {
            get
            {
                return droppedMoney;
            }
        }
    }
}