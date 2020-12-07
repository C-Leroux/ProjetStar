using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        private List<HealthSegment> segments; // List of all segments
        private int index;                    // Current last health segment used
        private float cur_health;
        private int smooth_index;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (smooth_index >= 0 && segments[smooth_index].IsEmpty())
            {
                --smooth_index;
            }
            if (smooth_index >= 0 && !segments[smooth_index].IsUpToDate())
            {
                segments[smooth_index].UpdateSmooth();
            }
        }

        public void SetMaxHealth(int health)
        {
            segments = new List<HealthSegment>();
            index = -1;
            for (int i = 0; i < health; i += 4)
            {
                GameObject newsegment = Instantiate(Resources.Load<GameObject>("Prefabs/UI/HealthSegment"), transform);
                HealthSegment segment = newsegment.GetComponent<HealthSegment>();
                if (health > i + 4)
                    segment.SetMaxHealth(4);
                else
                    segment.SetMaxHealth(health - i);
                segment.transform.parent = transform;
                segments.Add(segment);
                ++index;
            }

            segments[0].transform.localPosition = new Vector3();

            for (int i = 1; i < segments.Count; ++i)
            {
                if (i == 1)
                    segments[i].transform.localPosition = new Vector3(0, 1.52f, -0.5f);
                else if (i == 2)
                    segments[i].transform.localPosition = new Vector3(1.30f, 0.76f, -0.5f);
                else if (i == 3)
                    segments[i].transform.localPosition = new Vector3(1.30f, -0.76f, -0.5f);
                else if (i == 4)
                    segments[i].transform.localPosition = new Vector3(0, -1.52f, -0.5f);
                else if (i == 5)
                    segments[i].transform.localPosition = new Vector3(-1.30f, -0.76f, -0.5f);
                else if (i == 6)
                    segments[i].transform.localPosition = new Vector3(-1.30f, 0.76f, -0.5f);
            }

            cur_health = health;
            smooth_index = index;
        }

        public void SetHealth(float health)
        {
            float diff = cur_health - health;
            while (diff != 0)
            {
                float curSegmentHP = segments[index].CurHP();
                if (curSegmentHP > diff)
                {
                    segments[index].SetHealth(curSegmentHP - diff);
                    diff = 0;
                }
                else
                {
                    segments[index].SetHealth(0);
                    --index;
                    diff -= curSegmentHP;
                }
            }
        }
    }
}