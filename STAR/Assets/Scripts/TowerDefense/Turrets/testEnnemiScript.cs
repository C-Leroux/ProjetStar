using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnnemiScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.position + new Vector3(1f * speed, 0, 0);
    }
}
