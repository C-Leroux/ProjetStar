using System.Collections;
using System.Collections.Generic;
using System.Net;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Assets.Scripts
{
    public class TurretBehaviour : MonoBehaviour
    {
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


        public void init(Turret turret)
        {
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

        private void getEnnemiInRange()
        {
            //Collider2D[] allTargetInRange = Physics2D.OverlapCircleAll(transform.position, range);
            target = Physics2D.OverlapCircle(transform.position, range);
            try { target = target.gameObject.tag != "Ennemi" ? null : target; }
            catch (System.Exception e)
            {
                //La tourelle n'a pas de cible à portée
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