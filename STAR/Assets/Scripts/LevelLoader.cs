using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;
        public float transitionTime;
        private bool isLoading = false;
        private static LevelLoader instance;

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(transform.gameObject);
            }
        }

        public static LevelLoader Instance()
        {
            return instance;
        }

        public IEnumerator LoadLevel(string sceneName)
        {
            isLoading = true;

            // Play animation
            transition.SetTrigger("Start");

            // Wait
            yield return new WaitForSeconds(transitionTime);

            // Load scene
            StartCoroutine(LoadAsyncOperation(sceneName));
        }

        private IEnumerator LoadAsyncOperation(string sceneName)
        {
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneName);

            while (gameLevel.progress < 1)
            {
                // Loading animation

                yield return new WaitForEndOfFrame();
            }

            isLoading = false;
        }

        public IEnumerator EndLoad()
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);
        }

        public bool IsLoading()
        {
            return isLoading;
        }
    }
}