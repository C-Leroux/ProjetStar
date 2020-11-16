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

        private bool traveling = false;
        private static float speed = 3.0f;

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
            transform.localPosition = position.transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (traveling)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, position.transform.localPosition, speed * Time.deltaTime);
                if (transform.localPosition == position.transform.localPosition)
                {
                    traveling = false;
                    GameManager.Instance().TravelTo(position);
                }
            }
        }

        public static PlayerShip Instance()
        {
            return instance;
        }

        // The ship travel from its current position to the destination.
        public void Travel(SpaceObject destination)
        {
            if (!traveling)
            {
                CleanReachablePlanets();
                position = destination;
                FindReachablePlanets();
                traveling = true;
            }
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