﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Base
    {
        private static Base instance = null;// SINGLETON
        private int lp;
        private int lpMax;
        public Image healthbar;
        private Text healthbarText;

        public Base()
        {
            instance = this;
            lpMax = 1000;
            lp = 1000;
        }

        public static Base Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Base();
                }
                return instance;
            }
        }

        public void UpdateHealth()
        {
            lp = Mathf.Clamp(lp, 0, lpMax);
            float amount = (float)lp / lpMax;
            healthbar.fillAmount = amount;
            this.healthbarText.text = (lp + " / " + lpMax);
        }

        public int GetLp()
        {
            return lp;
        }

        public void SetHealthbar(Image healthbar)
        {
            this.healthbar = healthbar;
        }

        public void SetHealthbarText(Text healthbarText)
        {
            this.healthbarText = healthbarText;
            this.healthbarText.text = (lp + " / " + lpMax);
        }

        public bool IsAlive()
        {
            if (lp > 0)
            {
                return true;
            }
            return false;
        }

        public void ReceiveAttack(int attack)
        {
            lp -= attack;
            UpdateHealth();
        }

        public void AddMaxLp(int addLP)
        {
            this.lpMax += addLP;
        }

        public void ResetLp()
        {
            lp = lpMax;
        }
    }
}
