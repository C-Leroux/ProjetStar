using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthSegment : MonoBehaviour
    {
        public Image fill_green;
        public Image fill_red;
        private int maxHP = 4;
        private float curHP;
        private float smoothHP;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SetMaxHealth(int health = 4)
        {
            maxHP = health;
            smoothHP = health;
            fill_red.fillAmount = smoothHP / 4f;
            SetHealth(maxHP);
        }

        public void SetHealth(float health)
        {
            if (health > 4)
                health = 4f;
            curHP = health;
            fill_green.fillAmount = curHP / 4f;
        }

        public void UpdateSmooth()
        {
            smoothHP -= 2 * Time.deltaTime;
            if (smoothHP < curHP)
                smoothHP = curHP;
            fill_red.fillAmount = smoothHP / 4f;
        }

        public bool IsEmpty()
        {
            return smoothHP == 0;
        }

        public bool IsUpToDate()
        {
            return smoothHP == curHP;
        }    

        public float CurHP()
        {
            return curHP;
        }
    }
}