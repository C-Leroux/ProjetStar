using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyMusic : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find("MusicIntro(Clone)"))
        {
            GameObject test = GameObject.Find("MusicIntro(Clone)");
            Destroy(test);
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
