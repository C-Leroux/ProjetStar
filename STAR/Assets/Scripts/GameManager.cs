using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public SolarSystem solarSystem;
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
            InitSolarSystem();
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
            asyncLoadLevel = SceneManager.LoadSceneAsync("Plateau", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }
            InitBoard((Planet)destination);
            InitTD(boardScript.GetBoard(), (Planet)destination);
        }

        IEnumerator LoadChoice(SpaceObject destination)
        {
            asyncLoadLevel = SceneManager.LoadSceneAsync("VictoryChoice", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }
            InitChoice((Planet)destination);
        }
        
        
        IEnumerator LoadSolarSystem()
        {
            asyncLoadLevel = SceneManager.LoadSceneAsync("Empty");
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }
            solarSystem.gameObject.SetActive(true);
        }

        public void Victory(SpaceObject destination)
        {
            StartCoroutine("LoadChoice", destination);
        }

        public void ReturnToSolarSystem()
        {
            solarSystem.gameObject.SetActive(true);
            StartCoroutine("LoadSolarSystem");
        }

        public void TravelTo(SpaceObject destination)
        {
            solarSystem.gameObject.SetActive(false);
            // For marchands (or other SpaceObject types) : Find the type of the SpaceObject and execute the right portion of code
            StartCoroutine("LoadPlateau", destination);
        }
    }
}