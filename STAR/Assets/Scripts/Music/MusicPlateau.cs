using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MusicPlateau : MonoBehaviour
    {
        private static MusicPlateau instance;// SINGLETON
        private GameObject[] m_musics;
        private GameObject music;
        //private AudioSource audio_music;

        void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
            {
                m_musics = new GameObject[4];
                m_musics[0] = (GameObject)Resources.Load("Musics/MusicForest", typeof(GameObject));
                m_musics[1] = (GameObject)Resources.Load("Musics/MusicIce", typeof(GameObject));
                m_musics[2] = (GameObject)Resources.Load("Musics/MusicLava", typeof(GameObject));
                m_musics[3] = (GameObject)Resources.Load("Musics/MusicSand", typeof(GameObject));
                instance = this;
            }
        }

        public static MusicPlateau Instance()
        {
            return instance;
        }


        public void LaunchMusic(int choiceMusic)
        {
            music = (GameObject)m_musics[choiceMusic];
            SetVolume();
            Instantiate(music);
  
        }

        public void SetVolume()
        {
            //music.GetComponent<AudioSource>().volume = SaveSlider.Instance.GetVolume();
            AudioListener.volume = SaveSlider.Instance.GetVolume();
        }

    }
}
