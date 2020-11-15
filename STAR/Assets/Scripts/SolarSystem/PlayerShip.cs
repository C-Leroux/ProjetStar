using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerShip : MonoBehaviour
    {
        // The space object the ship is currently on
        public SpaceObject position;

        // Instance of the player ship
        private static PlayerShip instance;

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
                instance = this;
        }

        void Start()
        {
            FindReachablePlanets();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static PlayerShip Instance()
        {
            return instance;
        }

        // The ship travel from its current position to the destination.
        public void Travel(SpaceObject destination)
        {
            CleanReachablePlanets();
            position = destination;
            FindReachablePlanets();
        }

        private void ModifyReachablePlanets(bool b)
        {
            foreach (SpaceObject so in position.GetNeighbors())
            {
                so.Reachable = b;
            }
        }

        private void CleanReachablePlanets()
        {
            ModifyReachablePlanets(false);
        }

        private void FindReachablePlanets()
        {
            ModifyReachablePlanets(true);
        }
    }
}