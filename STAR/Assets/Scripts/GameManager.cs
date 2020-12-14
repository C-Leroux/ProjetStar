using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        private SolarSystem solarSystem;
        private BoardManager boardScript;
        private ChoiceManager choiceScript;
        private TDManager tdManager;
        private AsyncOperation asyncLoadLevel;
        
        private int level = 1;


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

        private void Start()
        {
        }

        public static GameManager Instance()
        {
            return instance;
        }

        void InitSolarSystem()
        {
            solarSystem.SetSolarSystem();
        }

        void InitBoard(Planet planet)
        {
            boardScript = BoardManager.Instance();
            boardScript.SetupScene(level, planet);
        }

        void InitChoice(Planet planet)
        {
            choiceScript = ChoiceManager.Instance;
            choiceScript.SetupChoice(planet);
        }

        void InitTD(int[,] board, Planet destination)
        {
            tdManager = TDManager.Instance();
            tdManager.Init(board, destination);
        }

        IEnumerator LoadPlateau(SpaceObject destination)
        {
            LevelLoader loader = LevelLoader.Instance();
            StartCoroutine(loader.LoadLevel("Plateau"));
            yield return new WaitForSeconds(1);
            while (loader.IsLoading())
                yield return null;

            solarSystem.gameObject.SetActive(false);
            InitBoard((Planet)destination);
            InitTD(boardScript.GetBoard(), (Planet)destination);
            StartCoroutine(loader.EndLoad());
            //print("Scene loaded");
        }

        IEnumerator LoadChoice(SpaceObject destination)
        {
            asyncLoadLevel = SceneManager.LoadSceneAsync("VictoryChoice", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }

            InitChoice((Planet)destination);

            /*LevelLoader loader = LevelLoader.Instance();
            StartCoroutine(loader.LoadLevel("Empty"));
            yield return new WaitForSeconds(1);
            while (loader.IsLoading())
                yield return null;

            StartCoroutine(loader.EndLoad());*/
        }
        
        
        public IEnumerator ReloadSolarSystem()
        {
            /*asyncLoadLevel = SceneManager.LoadSceneAsync("Empty");
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }*/

            LevelLoader loader = LevelLoader.Instance();
            StartCoroutine(loader.LoadLevel("Empty"));
            yield return new WaitForSeconds(1);
            while (loader.IsLoading())
                yield return null;

            solarSystem.gameObject.SetActive(true);

            StartCoroutine(loader.EndLoad());
        }

        public IEnumerator LoadSolarSystem()
        {
            /*asyncLoadLevel = SceneManager.LoadSceneAsync("Empty");
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }*/

            LevelLoader loader = LevelLoader.Instance();
            StartCoroutine(loader.LoadLevel("SolarSystem"));
            yield return new WaitForSeconds(1);
            while (loader.IsLoading())
                yield return null;

            solarSystem = GameObject.Find("Solar System").GetComponent<SolarSystem>();
            InitSolarSystem();
            solarSystem.gameObject.SetActive(true);

            StartCoroutine(loader.EndLoad());
        }

        IEnumerator LoadMerchant()
        {
            /*asyncLoadLevel = SceneManager.LoadSceneAsync("VilleMarchande");
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }*/

            LevelLoader loader = LevelLoader.Instance();
            StartCoroutine(loader.LoadLevel("VilleMarchande"));
            yield return new WaitForSeconds(1);
            while (loader.IsLoading())
                yield return null;

            StartCoroutine(loader.EndLoad());
        }

        public IEnumerator LoadMenu()
        {
            LevelLoader loader = LevelLoader.Instance();
            StartCoroutine(loader.LoadMenu());
            yield return new WaitForSeconds(1);
            while (loader.IsLoading())
                yield return null;

            if (solarSystem != null)
                Destroy(solarSystem.gameObject);
            Base.Reset();
            Player.Reset();
            Spell.Reset();
            Money.Reset();
            MoneyForMerchant.Reset();

            StartCoroutine(loader.EndLoad());
        }

        public IEnumerator LoadCredits()
        {
            LevelLoader loader = LevelLoader.Instance();
            StartCoroutine(loader.LoadLevel("Credits"));
            while (loader.IsLoading())
                yield return null;
        }
            public void Victory(SpaceObject destination)
        {
            StartCoroutine("LoadChoice", destination);
        }

        public void GoToSolarSystem()
        {
            StartCoroutine(LoadSolarSystem());
        }

        public void ReturnToSolarSystem()
        {
            PlayerShip.Instance().Clear();
            solarSystem.gameObject.SetActive(true);
            StartCoroutine("ReloadSolarSystem");
        }

        public void TravelTo(SpaceObject destination)
        {
            // For marchands (or other SpaceObject types) : Find the type of the SpaceObject and execute the right portion of code
            if (destination.GetType() == typeof(Merchant))
                StartCoroutine("LoadMerchant");
            else
                StartCoroutine("LoadPlateau", destination);
        }

        public void GoToCredits()
        {
            StartCoroutine("LoadCredits");
        }

        public void ReturnToMenu()
        {
            StartCoroutine("LoadMenu");
        }
    }
}