using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SaveSlider
    {
        private static SaveSlider instance = null;// SINGLETON
        //public Slider volumeSlider;

        public SaveSlider()
        {
            
        }

        public static SaveSlider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaveSlider();
                }
                return instance;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
