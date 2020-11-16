using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class SolarSystem : MonoBehaviour
    {
        // List of all the planets in the solar system. The solar system is made up of 6 levels (from the start to the end), and each level of 1 to 4 planets.
        // Each planet is linked to one, two or three planets of the previous level, and of the next level.
        private List<Planet> solar_system;

        // List of the number of planets in each rank. Starting point is rank 0, first reachable planets are rank 1, second are rank 2, etc.
        private List<int> rank_list;

        // The starting point of the player in the solar system.
        public StartPoint start_point;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            solar_system = new List<Planet>();
            rank_list = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };
        }

        public void SetSolarSystem()
        {
            // Here should be called the random generator for the planets of the solar system. For now, planets are created manually.
            // There are two generators : one for the solar system and its composition, and one for each planet individually.

            for (int i = 0; i < 15; ++i)
            {
                AddRandomPlanet();
            }

            AddPath(start_point, solar_system[0]);
            AddPath(start_point, solar_system[1]);
            AddPath(solar_system[0], solar_system[2]);
            AddPath(solar_system[0], solar_system[3]);
            AddPath(solar_system[1], solar_system[3]);
            AddPath(solar_system[1], solar_system[4]);
            AddPath(solar_system[2], solar_system[5]);
            AddPath(solar_system[2], solar_system[6]);
            AddPath(solar_system[3], solar_system[6]);
            AddPath(solar_system[3], solar_system[7]);
            AddPath(solar_system[4], solar_system[7]);
            AddPath(solar_system[4], solar_system[8]);
            AddPath(solar_system[5], solar_system[9]);
            AddPath(solar_system[6], solar_system[9]);
            AddPath(solar_system[6], solar_system[10]);
            AddPath(solar_system[7], solar_system[10]);
            AddPath(solar_system[7], solar_system[11]);
            AddPath(solar_system[8], solar_system[11]);
            AddPath(solar_system[9], solar_system[12]);
            AddPath(solar_system[10], solar_system[12]);
            AddPath(solar_system[10], solar_system[13]);
            AddPath(solar_system[11], solar_system[13]);
            AddPath(solar_system[12], solar_system[14]);
            AddPath(solar_system[13], solar_system[14]);

            Render();
        }

        private void AddPlanet(Planet new_planet)
        {
            solar_system.Add(new_planet);
            new_planet.transform.parent = gameObject.transform;
            new_planet.transform.position = new Vector3(0, 0, 0);
            new_planet.SetSprite();
        }

        // Generate a planet with a random biome and a random size
        private void AddRandomPlanet()
        {
            AddPlanet(Planet.CreateRandomPlanet());
        }

        // Add a path from an object to another one
        private void AddPath(SpaceObject object1, SpaceObject object2)
        {
            object1.AddNeighbor(object2);
        }

        /* Rendering */

        private void Render()
        {
            SetRanks();
            PlaceSpaceObjects();
            DrawAllLines();
        }

        private void SetRanks()
        {
            Queue<SpaceObject> queue = new Queue<SpaceObject>();
            start_point.Rank = 0;
            ++rank_list[0];
            queue.Enqueue(start_point);
            int currentRank = 1;

            while (queue.Count != 0)
            {
                SpaceObject current = queue.Dequeue();
                List<SpaceObject> neighbors_list = current.GetNeighbors();

                if (currentRank == current.Rank)
                    ++currentRank;

                foreach (SpaceObject so in neighbors_list)
                {
                    if (so.Rank == -1)
                    {
                        so.Rank = currentRank;
                        ++rank_list[currentRank];
                        queue.Enqueue(so);
                    }
                }
            }
        }

        private void PlaceSpaceObjects()
        {
            Queue<SpaceObject> queue = new Queue<SpaceObject>();
            queue.Enqueue(start_point);
            int currentRank = 0;

            while (queue.Count != 0)
            {
                int number_objects = rank_list[currentRank];
                float x = -0.5f * (number_objects - 1);
                float y = currentRank * 0.3f - 1.1f;

                for (int i = 0; i < number_objects; ++i)
                {
                    SpaceObject current = queue.Dequeue();
                    List<SpaceObject> neighbors_list = current.GetNeighbors();

                    current.transform.localPosition = transform.localPosition = new Vector3(x, y, 0);
                    foreach (SpaceObject so in neighbors_list)
                    {
                        if (!so.Placed)
                        {
                            so.Placed = true;
                            queue.Enqueue(so);
                        }
                    }
                    x += 1;
                }
                currentRank++;
            }
        }

        private void DrawAllLines()
        {
            DrawAllLinesRec(start_point);
        }

        private void DrawAllLinesRec(SpaceObject current)
        {
            if (current.Drawn)
                return;
            current.Drawn = true;
            foreach (SpaceObject so in current.GetNeighbors())
            {
                DrawLine(current, so);
                DrawAllLinesRec(so);
            }
        }

        private void DrawLine(SpaceObject from, SpaceObject to)
        {
            GameObject line = new GameObject("Line");
            LineRenderer lr = line.AddComponent<LineRenderer>();
            line.AddComponent<Line>();
            lr.transform.parent = transform;
            lr.startColor = Color.white;
            lr.endColor = Color.white;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, from.transform.position);
            lr.SetPosition(1, to.transform.position);
            lr.sortingLayerName = "Lines";
        }
    }
}