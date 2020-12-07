﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MusicPlateau : MonoBehaviour
    {
        private static MusicPlateau instance = null;// SINGLETON
        private GameObject[] m_musics;
        public int choice;

        public MusicPlateau()
        {
            m_musics = new GameObject[4];
            m_musics[0] = (GameObject)Resources.Load("Musics/MusicForest", typeof(GameObject));
            m_musics[1] = (GameObject)Resources.Load("Musics/MusicIce", typeof(GameObject));
            m_musics[2] = (GameObject)Resources.Load("Musics/MusicLava", typeof(GameObject));
            m_musics[3] = (GameObject)Resources.Load("Musics/MusicSand", typeof(GameObject));
            choice = 0;
        }

        public static MusicPlateau Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MusicPlateau();
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
            SetVolume();
        }

        public void LaunchMusic(int choiceMusic)
        {
            //Debug.Log("music" + choiceMusic);
            m_musics[choiceMusic].GetComponent<AudioSource>().volume = SaveSlider.Instance.GetVolume();
            Instantiate(m_musics[choiceMusic]);
            this.choice = choiceMusic;
        }

        public void SetVolume()
        {
            m_musics[choice].GetComponent<AudioSource>().volume = SaveSlider.Instance.GetVolume();
        }

    }
}
