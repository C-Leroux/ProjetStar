using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMusic : MonoBehaviour
{
    //public AudioClip[] soundtrack;
    public Slider volumeSlider;
    private ArrayList audioSources;
    AudioSource audioSource;


    /*void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }*/

    // Use this for initialization
    void Start()
    {
        audioSource = GameObject.Find("MusicIntro(Clone)").GetComponent<AudioSource>();
        audioSources = new ArrayList();
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
        audioSource.volume = sliderValue;
    }

   /* public void AddAudioSource(GameObject objectMusic)
    {
        audioSources.Add(objectMusic);
        audioSources[audioSources.Count].GetComponent<AudioSource>().volume = volumeSlider.value;
    }*/

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
