using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectionTurretMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void onClick()
    {
        this.gameObject.GetComponent<Image>().enabled = !this.gameObject.GetComponent<Image>().isActiveAndEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
