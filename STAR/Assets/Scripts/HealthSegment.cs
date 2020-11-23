using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthSegment : MonoBehaviour
    {
        public Image fill;
        private int maxHP = 4;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetHealth(int health)
        {
            if (health > 4)
                health = 4;
            fill.fillAmount = health / 4f;
        }
    }
}