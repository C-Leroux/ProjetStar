using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Wave : MonoBehaviour
    {
        private Queue<EnemyData> enemies;
        private Vector3 spawnPoint;
        private float delay;
        private float timer;
        private bool isActive = false;
        private int currentWave;
        private int nbEnemies = 0; // Total of enemies in this wave
        private int nbSpawned = 0; // Number of spawned enemies
        private int nbKilled = 0;  // Number of enemies killed

        // Use this for initialization
        void Start()
        {

            timer = 0f;
        }

        public void setDelay (float delay)
        {
            this.delay = delay;
        }
        public void setCurrentWave(int currentwave)
        {
            this.currentWave = currentwave;
        }
        // Update is called once per frame
        void Update()
        {
            if (isActive && nbSpawned < nbEnemies)
            {
                timer += Time.deltaTime;
                //Debug.Log("voici le delay " +delay + " pour la vague " +currentWave);
                if (timer > delay)
                {
                    Spawn();
                    ++nbSpawned;
                    timer = 0;
                }
            }
            if (GameObject.FindGameObjectsWithTag("Ennemi") == null)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        private void Spawn()
        {
            GameObject enemy_go = new GameObject("Enemy");
            Enemy enemy = enemy_go.AddComponent<Enemy>();
            enemy_go.AddComponent<BoxCollider2D>();
            enemy_go.GetComponent<BoxCollider2D>().size= new Vector2(0.25f, 0.4f);
            enemy_go.AddComponent<Rigidbody2D>();
            enemy_go.GetComponent<Rigidbody2D>().isKinematic = true;
            enemy_go.tag = "Ennemi";
            enemy_go.GetComponent<BoxCollider2D>().isTrigger = true;
            enemy_go.layer = 4; //MASK LAYER, FOR TURRET TARGET
            enemy_go.transform.parent = transform;
            enemy_go.transform.localPosition = spawnPoint;
            enemy_go.transform.localScale *= 5;
            enemy.LoadData(enemies.Dequeue(), currentWave);
            enemy.SetWave(this);
        }
  
        public void Despawn()
        {
            ++nbKilled;
            //Debug.Log("NBKIlL " + nbKilled + "/" + nbEnemies);
        }

        public bool IsCleared()
        {
            return nbEnemies <= nbKilled;
        }

        public void AddEnemy(EnemyData enemy)
        {
            enemies.Enqueue(enemy);
            ++nbEnemies;
        }

        public void Init(Queue<EnemyData> queue, Vector3 spawnPoint)
        {
            enemies = queue;
            nbEnemies = enemies.Count;
            this.spawnPoint = spawnPoint;
            isActive = true;
        }
    }
}