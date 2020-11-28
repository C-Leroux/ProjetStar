using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        private static BoardManager instance;

        public GameObject spaceship;
        public GameObject[] floorTiles;
        public GameObject[] objectTiles;
        public GameObject[] treeTiles;
        public GameObject[] foliageTiles;
        public int[,] board;
        public GameObject[] pathTiles;
        public GameObject test_vert;
        public Board m_board;
        private float timePassing;
        private float checkTime;
        private bool timerIsRunning;
        public Text timeText;
        public Text moneyText;
        public Text defeat;
        public Text victory;
        public Text healthbarText;
        public Image healthbar;
        public Image background;
        public Base m_base;
        private Planet planet;
        private int m_round;
        private int typeFloor;
        private int typePath;
        private int typeTree;
        private int typeFoliage;
        private int multiplicator;
        private List<Vector3> gridPositions = new List<Vector3>();
        private bool isGridSelectionCreated;


        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public static BoardManager Instance()
        {
            return instance;
        }

        //Gère l'arret et la reprise du temps
        public void ManageTime()
        {
            if (timerIsRunning)
            {
                timerIsRunning = false;
            }
            else
            {
                timerIsRunning = true;
            }
        }

        //Affichage du temps
        public void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }


        public void SetupScene(int level, Planet newplanet)
        {
            planet = newplanet;
            ManageTime();
            InitiateBoard();
        }

        public int[,] GetBoard()
        {
            return m_board.GetBoard();
        }

        void InitiateBoard()
        {
            m_board.InitBoardAlone(planet);
            m_board.InitialiseList2();
            m_board.LayoutTree(1, 2);
            m_board.LayoutTree(9, 1);
            m_board.LayoutTree(12, 6);
            //m_board.LayoutTree(11, 6);
            m_board.LayoutTree(10, 6);
            m_board.LayoutTree(7, 6);
            m_board.LayoutTree(9, 8);
            m_board.LayoutTree(10, 9);
            m_board.LayoutTree(11, 7);
            m_board.LayoutTree(3, 8);
            m_board.LayoutTree(12, 1);
            m_board.AffObject();
        }

        // Start is called before the first frame update
        void Start()
        {
            timePassing = 0;
            background.enabled = false;
            defeat.gameObject.SetActive(false);
            victory.gameObject.SetActive(false);
            Base.Instance.SetHealthbar(healthbar);
            Base.Instance.SetHealthbarText(healthbarText);
            Money.Instance.AddMoneyText(moneyText);
            Money.Instance.LaunchBoard();
            checkTime = 0;
            m_round = 1;
            timerIsRunning = false;
        }

    // Update is called once per frame
    void Update()
        {
            //IsSelectionTurretMode = true -> On selectionne des tourelles, sinon on ne selectionne pas
            if (m_board.GetSelectionTurretMode() && !isGridSelectionCreated) //Au Premier clic sur un bouton de tourelle, le mode selection tourelle est activé
            {             
                m_board.BeginningPoseTurret();
                isGridSelectionCreated = true; 
            }
            if (!m_board.GetSelectionTurretMode() && isGridSelectionCreated)//Au deuxième clic sur le même bouton de tourelle, le mode selection tourelle est désactivé
            {
                m_board.EndingPoseTurret();
                isGridSelectionCreated = false;
            }
            //DEBUG
            if (Input.GetKeyDown("space"))
            {
                ManageTime();
            }
            else if (Input.GetKeyDown("a"))
            {
                //m_base.ReceiveAttack(500);
                Base.Instance.ReceiveAttack(500);
            }
            else if(Input.GetKeyDown("z"))
            {
                m_board.BeginningPoseTurret();
            }
            else if(Input.GetKeyDown("e"))
            {
                m_board.EndingPoseTurret();
            }
            else if(Input.GetKeyDown("r"))
            {
                m_round++;
            }
            else if(Input.GetKeyDown("t"))
            {
                Money.Instance.AddMoney(500);
            }

            //Si la condition de victoire et defaite n'est pas atteinte
            if (timerIsRunning && Base.Instance.IsAlive() && !TDManager.Instance().IsWaveEnd())
            {
                timePassing += Time.deltaTime;
                if(checkTime < timePassing)
                {
                    checkTime++;
                    Money.Instance.UpdateMoneySecond();
                }
                DisplayTime(timePassing);
            }
            //Défaite
            else if(!Base.Instance.IsAlive())
            {
                background.enabled = true;
                defeat.gameObject.SetActive(true);
            }
            //Victoire
            else if(TDManager.Instance().IsWaveEnd())
            {
                background.enabled = true;
                victory.gameObject.SetActive(true);
            }
        }
    }
}
