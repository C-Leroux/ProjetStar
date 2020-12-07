using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SliderMusicPlateau : MonoBehaviour
    {
        //public AudioClip[] soundtrack;
        public Slider volumeSlider;

        // Use this for initialization
        void Start()
        {
            volumeSlider.value = SaveSlider.Instance.GetVolume();
        }

        void OnEnable()
        {
            //Register Slider Events
            volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
        }

        //Called when Slider is moved
        void changeVolume(float sliderValue)
        {
            SaveSlider.Instance.SetVolume(sliderValue);
            MusicPlateau.Instance.SetVolume();
        }

    }
}