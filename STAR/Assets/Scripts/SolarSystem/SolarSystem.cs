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
        public StartPoint start_point;

        // Start is called before the first frame update
        void Start()
        {
            // Here should be called the random generator for the planets of the solar system. For now, planets are created manually.
            // There are two generators : one for the solar system and its composition, and one for each planet individually.

            solar_system = new List<Planet>();
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

        // Update is called once per frame
        void Update()
        {

        }

        public void AddPlanet(Planet new_planet)
        {
            solar_system.Add(new_planet);
            new_planet.transform.parent = gameObject.transform;
            new_planet.transform.position = new Vector3(0, 0, 0);
        }

        // Generate a planet with a random biome and a random size
        public void AddRandomPlanet()
        {
            AddPlanet(Planet.CreateRandomPlanet());
        }

        // Add a path from an object to another one
        public void AddPath(SpaceObject object1, SpaceObject object2)
        {
            object1.AddNeighbor(object2);
        }

        /* Rendering */

        public void DrawLine(SpaceObject from, SpaceObject to)
        {
            GameObject line = new GameObject();
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.startColor = Color.white;
            lr.endColor = Color.white;
            lr.startWidth = 0.5f;
            lr.endWidth = 0.5f;
            lr.SetPosition(0, from.transform.position);
            lr.SetPosition(1, to.transform.position);
        }

        public void Render()
        {
            SpaceObject current = start_point;
            List<SpaceObject> neighbors_list = current.GetNeighbors();
            Stack<SpaceObject> stack = new Stack<SpaceObject>();
            int currentRank = 0;
            current.Rank = currentRank;

            while (neighbors_list.Count != 0)
            {
                foreach (SpaceObject so in neighbors_list)
                {
                    if (so.Rank == -1)
                        so.Rank = currentRank;
                    stack.Push(so);

                    DrawLine(current, so);
                }
            }
        }
    }
}