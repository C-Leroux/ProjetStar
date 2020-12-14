using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusicBackground : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find("MusicBackground(Clone)"))
        {
            GameObject test = GameObject.Find("MusicBackground(Clone)");
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
