using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SaveSlider
    {
        private static SaveSlider instance = null;// SINGLETON
        private float volume;
        //public Slider volumeSlider;

        public SaveSlider()
        {
            volume = 1f;
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

        public void SetVolume(float newVolume)
        {
            volume = newVolume;
            Debug.Log(volume);
        }
        
        public float GetVolume()
        {
            return volume;
        }

    }
}
