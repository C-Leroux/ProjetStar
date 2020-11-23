using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyData enemyData;
        private Wave wave;
        private int targetIndex = 0;
        private int hp;

        private static List<Vector3> path;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!wave)
                return;
            if (CanAttackBase())
                AttackBase();
            else
                Walk();
        }

        public void LoadData(EnemyData data)
        {
            enemyData = data;
            hp = data.MaxHP;
        }

        public void SetWave(Wave wave)
        {
            this.wave = wave;
        }

        private void Walk()
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, path[targetIndex], SpeedFormula() * Time.deltaTime);
            if (transform.localPosition == path[targetIndex])
            {
                ++targetIndex;
            }
        }

        private bool CanAttackBase()
        {
            return Vector2.Distance(transform.localPosition, path[path.Count - 1]) <= RangeFormula();
        }

        private void AttackBase()
        {

        }

        private void TakeDamages(int damages)
        {
            hp -= damages;
            if (hp < 0)
            {
                wave.Despawn();
            }
        }

        public static void SetPath(List<Vector3> newpath)
        {
            path = newpath;
        }

        private float RangeFormula()
        {
            return (enemyData.AtkRange / 2f) + 2f;
        }

        private float SpeedFormula()
        {
            return (enemyData.Speed / 2);
        }
    }
}