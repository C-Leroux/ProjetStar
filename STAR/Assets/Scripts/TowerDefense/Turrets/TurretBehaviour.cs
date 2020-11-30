using System.Collections;
using System.Collections.Generic;
using System.Net;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Assets.Scripts
{
    public class TurretBehaviour : MonoBehaviour
    {
        private string name;
        private ProjectileBehaviour projectileTemplate;
        private Projectile projectileToSpawn;
        private float range;
        private int cost;
        private Transform turretPosition;
        private float attack_speed;
        private float scale;
        private float readyTime;
        private float current_time;
        private Collider2D target;


        [SerializeField]
        private Sprite[] turretSpriteUpgrade;
        [SerializeField]
        private LayerMask ennemis;

        

        public void init(Turret turret)
        {
            this.name = turret.name;
            this.cost = turret.cost;
            this.projectileTemplate = turret.projectileTemplate;
            this.projectileToSpawn = turret.projectileToSpawn;
            this.range = turret.range;
            this.turretPosition = this.transform;
            this.attack_speed = 1 / turret.attack_speed;
            this.readyTime = turret.readyTime;
            this.scale = turret.scale;
            this.GetComponent<SpriteRenderer>().sprite = turret.sprite;
            this.transform.localScale = new Vector3(scale, scale, scale);

        }

        // Start is called before the first frame update
        void Start()
        {
            current_time = 0f;
            gameObject.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, range);
        }

       

        private bool IsInRange(Transform point)
        {

            return Vector3.Distance(point.position, transform.position) <= range;
        }




        //GetSelectedTowerInfo
        public string getTurretSelectedName()
        {
            return this.name;
        }

        public int getTurretSelectedCost()
        {
            return this.cost;
        }

        private void getEnnemiInRange()
        {
            //Collider2D[] allTargetInRange = Physics2D.OverlapCircleAll(transform.position, range);
            target = Physics2D.OverlapCircle(transform.position, range, ennemis);
            try {
            target = target.gameObject.tag != "Ennemi" ? null : target;
            }
            catch (System.Exception e)
            {
                //La tourelle n'a pas de cible à portée
            }

        }


        //SETTERS FOR UPGRADE

        public void setNameForUpgrade(string name)
        {
            this.name = name;
        }
        public void setAttackSpeedForUpgrade(float attackSpeed)
        {
            this.attack_speed = attackSpeed;
        }
        public void setRangeForUpgrade(float range)
        {
            this.range = range;
        }
        public void setCostForUpgrade(int upgradedCost)
        {
            this.cost = cost + upgradedCost;
        }     
        public void setSpriteForUpgrade(string turretName)
        {
            switch (turretName)
            {
                case "STAR": //TOURELLE DE LA STAR
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[0];
                    break;
                case "STAR1": //TOURELLE DE LA STAR NIVEAU 1
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[1];
                    break;
                case "STAR2": //TOURELLE DE LA STAR NIVEAU MAX
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[2];
                    break;

                case "Grenadiere": //Grenadiere
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[3];
                    break;
                case "Grenadiere1": //Grenadiere NIVEAU 1
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[4];
                    break;
                case "Grenadiere2": //Grenadiere NIVEAU MAX
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[5];
                    break;

                case "Cryomancienne": //Cryomancienne
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[6];
                    break;
                case "Cryomancienne1": //Cryomancienne NIVEAU 1
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[7];
                    break;
                case "Cryomancienne2": //Cryomancienne NIVEAU MAX
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[8];
                    break;

                case "Empoisonneuse": //Empoisonneuse
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[9];
                    break;
                case "Empoisonneuse1": //Empoisonneuse NIVEAU 1
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[10];
                    break;
                case "Empoisonneuse2": //Empoisonneuse NIVEAU MAX
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[11];
                    break;

                case "Pyromancienne": //Pyromancienne
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[12];
                    break;
                case "Pyromancienne1": //Pyromancienne NIVEAU 1
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[13];
                    break;
                case "Pyromancienne2": //Pyromancienne NIVEAU MAX
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[14];
                    break;

                case "Survolteuse": //Survolteuse
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[15];
                    break;
                case "Survolteuse1": //Survolteuse NIVEAU 1
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[16];
                    break;
                case "Survolteuse2": //Survolteuse NIVEAU MAX
                    this.GetComponent<SpriteRenderer>().sprite = turretSpriteUpgrade[17];
                    break;
            }
        
        }



        private void followTarget()
        {
            Vector3 dir = target.transform.position - transform.position;
            Debug.DrawRay(transform.position, dir, Color.red);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            turretPosition.rotation = Quaternion.Lerp(turretPosition.rotation,
            Quaternion.AngleAxis(angle, Vector3.forward), 0.09f);
        }

        private void Fire()
        {
            //La tourelle est l'équivalent du turretManager pour les projectiles, c'est un projectilesManager
            current_time = current_time - attack_speed;
            GameObject bullet = GameObject.Instantiate(projectileTemplate.gameObject);
            bullet.transform.position = turretPosition.position;
            ProjectileBehaviour behaviour = bullet.GetComponent<ProjectileBehaviour>();
            behaviour.init(projectileToSpawn);
            behaviour.updateProjectileDamageUpgraded(this.name);
            behaviour.initFire(target);
        }




        // Update is called once per frame
        void Update()
        {
            if (target == null || !IsInRange(target.transform))
            {
                getEnnemiInRange();
            }
            current_time += Time.deltaTime;
            if (target != null)
            {
                followTarget();
                if (current_time >= attack_speed)
                {
                    Fire();
                }
            }
            else if (current_time >= attack_speed)
            {
                current_time = attack_speed - readyTime;
            }
        }

    }
}