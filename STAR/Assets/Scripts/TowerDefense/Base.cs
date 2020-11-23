using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Base : MonoBehaviour
    {
        //private static Base instance;
        private int lp;
        private int lpMax;
        private Image healthbar;
        private Text healthbarText;

        public Base(Image healthbar, Text healthbarText)
        {
            lpMax = 1000;
            lp = 1000;
            //Canvas canvas = GetComponent<Canvas>();

            //Debug.Log(""+canvas);
            this.healthbar = healthbar;
            this.healthbarText = healthbarText;
            this.healthbarText.text = (lp + " / " + lpMax);

        }

        /*public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }*/

        // Start is called before the first frame update
        void Start()
        {
  
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateHealth()
        {
            lp = Mathf.Clamp(lp, 0, lpMax);
            float amount = (float)lp / lpMax;
            healthbar.fillAmount = amount;
            this.healthbarText.text = (lp + " / " + lpMax);
        }

        /*public static Base Instance()
        {
            if (instance == null)
            {
                Debug.Log("ci");
                this.instance = new Base();
            }
            return instance;
        }*/

        public int GetLp()
        {
            return lp;
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
    }
}
