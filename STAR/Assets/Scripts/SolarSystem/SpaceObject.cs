using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    // A space object is an entity that can appear on the solar system. It can be either a planet or a starting point for exemple.
    // A space object have a list of neighbor that can be accessed from there by the player.
    public class SpaceObject : MonoBehaviour
    {
        private List<SpaceObject> neighbors = new List<SpaceObject>();

        // Variables used to construct the solar system
        public int Rank { get; set; } = -1;
        public bool Placed { get; set; } = false;
        public bool Drawn { get; set; } = false;

        // True if the player is on a space object that has this one as a neighbor
        public bool Reachable { get; set; } = false;

        protected SpriteRenderer render;

        // Animation variables
        protected bool isMouseHover = false;
        protected Vector3 initialScale;
        protected Vector3 hoverScale;
        protected float tick;
        static protected float speed = 3.0f;
        static protected float scaleMult = 1.3f;

        void Awake()
        {
            render = gameObject.AddComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            tick += Time.deltaTime;
            if (tick > 1)
                tick = 0;

            if (isMouseHover && Reachable)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, hoverScale, speed * Time.deltaTime);
            }
            else if (Reachable)
            {
                if (tick < 0.5)
                    transform.localScale = Vector3.Lerp(transform.localScale, hoverScale * 0.9f, (speed / 2) * Time.deltaTime);
                else
                    transform.localScale = Vector3.Lerp(transform.localScale, initialScale, (speed / 2) * Time.deltaTime);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, initialScale, speed * Time.deltaTime);
            }
        }

        private void OnMouseEnter()
        {
            isMouseHover = true;
        }

        private void OnMouseExit()
        {
            isMouseHover = false;
        }

        private void OnMouseUp()
        {
            if (Reachable)
            {
                PlayerShip.Instance().Travel(this);
            }
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

        public void SetVisited()
        {
            render.color = new Color(0.5f, 0.5f, 0.5f);
            GameObject complete = (GameObject)Instantiate(Resources.Load("Prefabs/UI/complete"), transform, false);
            complete.transform.localPosition = new Vector3();
            SpriteRenderer rend = complete.GetComponent<SpriteRenderer>();
            rend.sortingLayerName = "Sprites";
            rend.sortingOrder = 1;
        }

        public void SetSprite(string path, float scale)
        {
            render.sprite = Resources.Load<Sprite>(path);
            render.sortingLayerName = "Sprites";

            gameObject.transform.localScale = new Vector3(scale, scale, 0);
            initialScale = transform.localScale;
            hoverScale = transform.localScale * scaleMult;
            gameObject.AddComponent<BoxCollider2D>();
        }
    }
}