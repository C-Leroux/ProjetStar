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
        private HealthBar healthbar;
        private int targetIndex = 0;
        private float hp;
        private bool poisonned;
        private bool stun;
        private static List<Vector3> path;
        private string direction;
        private int positionCounter;
        private float movingSpeedWalkingAnimation;
        private float current_time;
        private float attack_speed;
        private int currentWave;

        // Use this for initialization
        void Start()
        {
            poisonned = false;
            stun = false;
            SpriteRenderer sprite = gameObject.AddComponent<SpriteRenderer>();
            sprite.sprite = enemyData.Sprite;
            direction = "right";
            positionCounter = 0;
            movingSpeedWalkingAnimation = 0.25f;
            current_time = 0f;
            attack_speed =1/0.9f; //Pour l'instant pareil pour tous les ennmis, à rajouter dans Ennemi_Data si on veut
            if (currentWave <= 5)
            {
                enemyData.maxHP = enemyData.MaxHP + currentWave / 2;
            }
            if (currentWave >= 5)
            {
                enemyData.maxHP = enemyData.MaxHP + currentWave;
            }
        }

        public void updateEnnemyHpWithWave(int currentWave)
        {
          
        }

        // Update is called once per frame
        void Update()
        {
            if (BoardManager.Instance().timerIsRunning)
            {
                current_time += Time.deltaTime;
                if (!wave)
                    return;
                if (CanAttackBase() && current_time >= attack_speed && !stun)
                {
                    AttackBase();
                }
                if (!stun && !CanAttackBase())
                {
                    Walk();
                    current_time = attack_speed;

                }

                //Changing Color sprite according to ennemi state
                changeSpriteColor();
            }
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
        IEnumerator WaitBeforeUnPoisonned(float poisonnedTime) //On lance de décompte, une fois le poisonnedTime écoulé, la cible n'est plus empoisonnée
        {
            yield return new WaitForSeconds(poisonnedTime);
            poisonned = false;
        }
        IEnumerator WaitBeforeDealDamagePoisonned(float damage, float poisonnedTime)
        {
            for (int i = 0; i < poisonnedTime; i++)
            {
                TakeDamages(damage);
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


        public void LoadData(EnemyData data, int currentWave)
        {

            enemyData = data;
            hp = data.MaxHP;
            SetHealthBar();
        }

        private void SetHealthBar()
        {
            GameObject go = new GameObject();
            Canvas canvas = go.AddComponent<Canvas>();
            canvas.transform.SetParent(transform);
            canvas.transform.localPosition = new Vector3(0, 0.3f, 0);
            canvas.renderMode = RenderMode.WorldSpace;
            GameObject healthBarObj = new GameObject("Healthbar");
            healthBarObj.transform.SetParent(canvas.transform);
            healthBarObj.transform.localPosition = new Vector3();
            healthbar = healthBarObj.AddComponent<HealthBar>();
            healthbar.SetMaxHealth((int)hp);
        }

        public void SetWave(Wave wave)
        {
            this.wave = wave;
        }


        private string updateEnnemiDirection(Vector2 currentPosition, Vector2 nextPosition) //ANIMATION ENNEMI
        {
            
            if (currentPosition.x < nextPosition.x)
            {
                //Sprite va vers la droite
                this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[2];
                return "right";
            }
            if (currentPosition.x > nextPosition.x)
            {
                //Sprite va vers la gauche
                this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[1];
                return "left";
            }
            if (currentPosition.y < nextPosition.y)
            {
                //Sprite va vers la haut
                this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[3];
                return "up";
            }
            if (currentPosition.y > nextPosition.y)
            {
                //Sprite va vers la bas
                return "down";
            }
            return null;
        }

        public void setCounterPositionAfterDelay(float walkingSpeedAnimation, int couterPositionValue) //ANIMATION
        {
            StartCoroutine(waitBeforeWalk(walkingSpeedAnimation, couterPositionValue));
        }

        IEnumerator waitBeforeWalk(float walkingSpeedAnimation, int couterPositionValue)
        {
            yield return new WaitForSeconds(walkingSpeedAnimation);
            positionCounter = couterPositionValue;
        } //ANIMATION



        private void Walk()
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, path[targetIndex], SpeedFormula() * Time.deltaTime);
            if (transform.localPosition == path[targetIndex] && targetIndex + 1 < path.Count)
            {
                ++targetIndex;
            }
            Vector2 currentPosition = transform.localPosition;
            Vector2 nextPosition = Vector3.MoveTowards(transform.localPosition, path[targetIndex], SpeedFormula() * Time.deltaTime);
           //ANIMATION
            if (currentPosition != nextPosition)
            {
                direction = updateEnnemiDirection(currentPosition, nextPosition);
            
            }
          
                  switch (direction)
            {
                case "down":
                    switch (positionCounter)
                    {
                        case 0:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[0];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 1);
                            break;
                        case 1:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[1];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 2);
                            break;
                        case 2:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[2];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 0);
                            break;
                    }
                     break;
                case "right":
                    switch (positionCounter)
                    {
                        case 0:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[6];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 1);
                            break;
                        case 1:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[7];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 2);
                            break;
                        case 2:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[8];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 0);
                            break;
                    }
                    break;
                case "left":
                    switch (positionCounter)
                    {
                        case 0:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[3];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 1);
                            break;
                        case 1:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[4];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 2);
                            break;
                        case 2:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[5];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 0);
                            break;
                    }
                    break;
                case "up":
                    switch (positionCounter)
                    {
                        case 0:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[9];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 1);
                            break;
                        case 1:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[10];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 2);
                            break;
                        case 2:
                            this.GetComponent<SpriteRenderer>().sprite = enemyData.spritesToDirection[11];
                            setCounterPositionAfterDelay(movingSpeedWalkingAnimation, 0);
                            break;
                    }
                    break;
            }

        }

        private bool CanAttackBase()
        {
            return Vector2.Distance(transform.localPosition, path[path.Count - 1]) <= RangeFormula();
        }

        private void AttackBase()
        {
            current_time = current_time - attack_speed;
            Base.Instance.ReceiveAttack(this.enemyData.AtkDamage);
        }

        public void TakeDamages(float damages)
        {
            hp -= damages;
            if (hp <= 0)
            {
                hp = 0;
                wave.Despawn();
                Money.Instance.AddMoney(enemyData.DroppedMoney);
                soundEffectHelper.Instance.destroyEnnemi();
                SpecialEffectsHelper.Instance.destroyEnnemi(this.transform.position);
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                SpecialEffectsHelper.Instance.EnnemiHit(this.transform.position);
                //soundEffectHelper.Instance.EnnemiHit();
                UpdateHealthBar();
            }
        }

        private void UpdateHealthBar()
        {
                       healthbar.SetHealth(hp);
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