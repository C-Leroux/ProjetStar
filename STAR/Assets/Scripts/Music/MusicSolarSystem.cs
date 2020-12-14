using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MusicSolarSystem : MonoBehaviour
    {
        public GameObject musique;
        void Awake()
        {
            if (!GameObject.Find("MusicBackground(Clone)"))
            {
                Instantiate(musique);
                AudioListener.volume = SaveSlider.Instance.GetVolume();
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