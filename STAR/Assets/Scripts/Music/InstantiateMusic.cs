using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class InstantiateMusic : MonoBehaviour
    {
        public GameObject musique;
        void Awake()
        {
            if (!GameObject.Find("MusicIntro(Clone)"))
            {
                Instantiate(musique);
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
