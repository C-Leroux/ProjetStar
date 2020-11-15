using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Line : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            Destroy(gameObject.GetComponent<LineRenderer>().material);
        }
    }
}