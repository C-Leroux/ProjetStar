using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts
{
    public class WaveGenerator
    {
        private List<EnemyData> possibleEnemies;
        private Vector3 spawnPoint;
        private int nbEnemies;
        private int nbWaves;
        private int currentWave = 0;
        private GameObject board;

        public WaveGenerator(Planet planet, Vector3 spawnPoint)
        {
            SelectEnemies(planet);
            this.spawnPoint = spawnPoint;
            nbEnemies = planet.Rank * 5;
            nbWaves = planet.Rank * 5;
            board = GameObject.Find("Board");
        }

        // Select all possible enemies in this wave
        private void SelectEnemies(Planet planet)
        {
            possibleEnemies = new List<EnemyData>();
            string path = "ScriptableObjects/Enemy Data/";
            foreach (EnemyData enemy in Resources.LoadAll(path + "Commun"))
            {
                possibleEnemies.Add(enemy);
            }
            switch(planet.biome)
            {
                case Planet.Biome.forest:
                    foreach (EnemyData enemy in Resources.LoadAll(path + "Forest"))
                    {
                        possibleEnemies.Add(enemy);
                    }
                    break;
                case Planet.Biome.desert:
                    foreach (EnemyData enemy in Resources.LoadAll(path + "Desert"))
                    {
                        possibleEnemies.Add(enemy);
                    }
                    break;
                case Planet.Biome.ice:
                    foreach (EnemyData enemy in Resources.LoadAll(path + "Ice"))
                    {
                        possibleEnemies.Add(enemy);
                    }
                    break;
                case Planet.Biome.fire:
                    foreach (EnemyData enemy in Resources.LoadAll(path + "Fire"))
                    {
                        possibleEnemies.Add(enemy);
                    }
                    break;
            }
        }

        // Generate a wave using stored informations
        public Wave GenerateWave()
        {
            GameObject go = new GameObject("Wave");
            go.transform.parent = board.transform;
            Wave wave = go.AddComponent<Wave>();
            Queue<EnemyData> queue = new Queue<EnemyData>();
            System.Random random = new System.Random();
            for (int i = 0; i < nbEnemies; ++i)
            {
                int index = random.Next(possibleEnemies.Count);
                queue.Enqueue(possibleEnemies[index]);
            }
            wave.Init(queue, spawnPoint);
            ++currentWave;
            return wave;
        }
    }
}