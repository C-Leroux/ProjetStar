using System.Collections;
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
        private float hp;
        private bool poisonned;
        private bool stun;
        private static List<Vector3> path;

        // Use this for initialization
        void Start()
        {
            poisonned = false;
            stun = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!wave)
                return;
            if (CanAttackBase())
                AttackBase();
            else if (!stun)
            {
                Walk();
            }
            //Changing Color sprite according to ennemi state
            changeSpriteColor();    

        }
        //Poisonned Methods
        public bool getPoisonned()
        {
            return poisonned;
        }
        public void setPoisonnedTrue(float damage, float poisonnedTime) //La cible est empoisonnée
        {
            poisonned = true;
            StartCoroutine(WaitBeforeUnPoisonned(poisonnedTime));
            StartCoroutine(WaitBeforeDealDamagePoisonned(damage, poisonnedTime));
        }
        IEnumerator WaitBeforeUnPoisonned(float poisonnedTime) //On lance de décompte, une fois le poisonnedTime écoulé, la cilbe n'est plus empoisonnée
        {
            yield return new WaitForSeconds(poisonnedTime);
            poisonned = false;
        }

        IEnumerator WaitBeforeDealDamagePoisonned(float damage, float poisonnedTime)
        {
            for (int i = 0; i < poisonnedTime; i++)
            {
                TakeDamages(damage);
                Debug.Log("DAMAGE TAKEN FROM EMPOISONNEUSE EACH SECOND");
                yield return new WaitForSeconds(1f); //On attend une seconde, puis on fait les dégats du poisson
            }
        }


        //Stun Methods
        public bool getStun()
        {
            return stun;
        }
        public void setStunTrue(float stunTime) //La cible est étourdie
        {
            stun = true;
            StartCoroutine(WaitBeforeUnStun(stunTime));
        }

        IEnumerator WaitBeforeUnStun(float stunTime) //On lance de décompte, une fois le stunTime écoulé, la cilbe n'est plus étourdie
        {
            yield return new WaitForSeconds(stunTime);
            stun = false;
        }

        public void changeSpriteColor()
        {
            if (stun && !poisonned)
            {
                this.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            if (!stun && poisonned)
            {
                this.GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (stun && poisonned)
            {
                this.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            if (!stun && !poisonned)
            {
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
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
            if (transform.localPosition == path[targetIndex] && targetIndex + 1 < path.Count)
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

        public void TakeDamages(float damages)
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
            return (enemyData.AtkRange / 2f) + 2.5f;
        }

        private float SpeedFormula()
        {
            return (enemyData.Speed / 2);
        }
    }
}