﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MusicCredit : MonoBehaviour
    {
        public GameObject musique;
        AudioSource audioSource;
        bool end;

        void Awake()
        {
            Instantiate(musique);
            audioSource = GameObject.Find("MusicCredit(Clone)").GetComponent<AudioSource>();
            AudioListener.volume = SaveSlider.Instance.GetVolume();
            end = false;

        }
        // Start is called before the first frame update
        void Start()
        {
     
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("escape")) { 
                GameManager.Instance().ReturnToMenu();
            }
            if (!audioSource.isPlaying && !end)
            {
                end = true;
                GameManager.Instance().ReturnToMenu();
            }
        }
    }
}