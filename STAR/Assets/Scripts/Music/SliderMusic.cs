using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SliderMusic : MonoBehaviour
    {
        //public AudioClip[] soundtrack;
        public Slider volumeSlider;
        AudioSource audioSource;


        /*void Awake()
        {

        }*/

        // Use this for initialization
        void Start()
        {
            audioSource = GameObject.Find("MusicIntro(Clone)").GetComponent<AudioSource>();
            volumeSlider.value = SaveSlider.Instance.GetVolume();
            changeVolume(volumeSlider.value);
        }

        // Update is called once per frame
        void Update()
        {
            /*if (!audioSource.isPlaying)
            {
                audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
                audioSource.Play();
            }*/
        }

        void OnEnable()
        {
            //Register Slider Events
            volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
        }

        //Called when Slider is moved
        void changeVolume(float sliderValue)
        {
            Debug.Log("Slider Value" + sliderValue);
            audioSource.volume = sliderValue;
            SaveSlider.Instance.SetVolume(sliderValue);
        }

        public void SetSlider(Slider slider)
        {
            this.volumeSlider = slider;
        }

        void OnDisable()
        {
            //Un-Register Slider Events
            // volumeSlider.onValueChanged.RemoveAllListeners();
        }
    }

}