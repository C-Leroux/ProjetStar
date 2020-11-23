using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        private List<HealthSegment> segments;
        private int cur_health;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetMaxHealth(int health)
        {
            segments = new List<HealthSegment>();
            for (int i = 0; i < health; i += 4)
            {
                GameObject newsegment = Resources.Load<GameObject>("Prefabs/UI/HealthSegment");
                HealthSegment segment = newsegment.GetComponent<HealthSegment>();
                if (health > i + 4)
                    segment.SetHealth(4);
                else
                    segment.SetHealth(health - i);
                segment.transform.parent = transform;
                segments.Add(segment);
            }

            segments[0].transform.position = new Vector3();

            for (int i = 1; i < segments.Count; ++i)
            {
                if (i == 1)
                    segments[i].transform.position = new Vector3(0, 152, -0.5f);
                else if (i == 2)
                    segments[i].transform.position = new Vector3(130, 76, -0.5f);
                else if (i == 3)
                    segments[i].transform.position = new Vector3(130, -76, -0.5f);
                else if (i == 4)
                    segments[i].transform.position = new Vector3(0, -152, -0.5f);
                else if (i == 5)
                    segments[i].transform.position = new Vector3(-130, -76, -0.5f);
                else if (i == 6)
                    segments[i].transform.position = new Vector3(-130, 76, -0.5f);
            }

            cur_health = health;
        }

        public void SetHealth(int health)
        {
            int diff = cur_health - health;
            while (diff != 0)
            {
                //diff = 
            }
        }
    }
}