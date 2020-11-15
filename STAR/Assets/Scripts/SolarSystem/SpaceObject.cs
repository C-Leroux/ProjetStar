using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    // A space object is an entity that can appear on the solar system. It can be either a planet or a starting point for exemple.
    // A space object have a list of neighbor that can be accessed from there by the player.
    public class SpaceObject : MonoBehaviour
    {
        private List<SpaceObject> neighbors = new List<SpaceObject>();
        public int Rank { get; set; } = -1;
        public bool Placed { get; set; } = false;
        public bool Drawn { get; set; } = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public List<SpaceObject> GetNeighbors()
        {
            return neighbors;
        }

        public void AddNeighbor(SpaceObject new_neighbor)
        {
            if (!neighbors.Contains(new_neighbor))
            {
                neighbors.Add(new_neighbor);
            }
        }
    }
}