using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LoadingAnimator : MonoBehaviour
    {
        [SerializeField]
        private Text loadingText;

        [SerializeField]
        private Image loadingImage;
        private float y0;

        private List<Sprite> sprites;
        private int index = 0;

        private bool isLoading = false;

        // Start is called before the first frame update
        void Start()
        {
            sprites = new List<Sprite>();
            string path = "Sprites/Solar_system";
            foreach (Sprite sprite in Resources.LoadAll<Sprite>(path))
            {
                sprites.Add(sprite);
            }

            y0 = loadingImage.transform.position.y;
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (isLoading)
            {
                loadingImage.transform.position = new Vector3(loadingImage.transform.position.x, y0 + 15 * Mathf.Sin(3 * Time.time), 0);
            }
        }

        public void BeginLoad()
        {
            gameObject.SetActive(true);
            isLoading = true;
        }

        public void EndLoad()
        {
            gameObject.SetActive(false);
            isLoading = false;
            ++index;
            if (index == sprites.Count)
                index = 0;
            loadingImage.sprite = sprites[index];
        }
    }
}
