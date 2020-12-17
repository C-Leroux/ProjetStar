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
        private int currentPlanetRank;
        public WaveGenerator(Planet planet, Vector3 spawnPoint)
        {
            currentPlanetRank = planet.Rank;
            SelectEnemies(planet);
            this.spawnPoint = spawnPoint;
            nbWaves = planet.Rank * 5;
            nbEnemies = 3;
            

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
            switch (planet.biome)
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
            EnemyData liche = (EnemyData)Resources.Load("ScriptableObjects/Enemy Data/Boss/Lich");
            EnemyData finalBoss = (EnemyData)Resources.Load("ScriptableObjects/Enemy Data/Boss/Final_Boss");
            GameObject go = new GameObject("Wave");
            go.transform.parent = board.transform;
            Wave wave = go.AddComponent<Wave>();
            Queue<EnemyData> queue = new Queue<EnemyData>();
            System.Random random = new System.Random();
            if (currentWave == 0)
            {
                nbEnemies = 3;
            }
            else
            {
                nbEnemies = nbEnemies + currentWave * currentPlanetRank;
            }
            for (int i = 0; i < nbEnemies; ++i)
            {
                int index = random.Next(possibleEnemies.Count);
                queue.Enqueue(possibleEnemies[index]);
            }
            if (currentWave == nbWaves-1)
            {
                int i = 0;
                while (i < currentPlanetRank)
                {
                    queue.Enqueue(liche);
                    i++;
                }
                if (currentPlanetRank ==5 )
                {
                    queue.Enqueue(finalBoss);
                }
            }
            wave.Init(queue, spawnPoint);
            if (currentWave ==0 || currentWave==1 )
            {
                wave.setDelay(2f);
            }
            else
            {
                wave.setDelay(2f / (currentWave / 2));
            }
            wave.setCurrentWave(currentWave);
            ++currentWave;
            return wave;
        }

        public int GetCurrentWave()
        {
            return currentWave;
        }

        public int GetNbWaves()
        {
            return nbWaves;
        }

    }
}