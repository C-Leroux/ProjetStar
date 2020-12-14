using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class introController : MonoBehaviour
    {

        public float wait_time = 10f;

        void Start()
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(wait_time);

            //SceneManager.LoadScene("MainMenu");

            GameManager.Instance().ReturnToMenu();
        }
    }
}
