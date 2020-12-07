using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;

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
        public Text timeText;
        public Text moneyText;
        public Text defeat;
        public Text victory;
        public Text healthbarText;
        public Image healthbar;
        public Image background;
        public Image[] m_imageTurrets;
        public Image[] m_imageBackground;
        public Image[] m_imageCaches;
        public Base m_base;
        public Image rToMenu;
        public Text rToMenuText;
        public Image rToMenuCache;
        private float timePassing;
        private float checkTime;
        public bool timerIsRunning;
        private Planet planet;
        private int typeFloor;
        private int typePath;
        private int typeTree;
        private int typeFoliage;
        private int multiplicator;
        private List<Vector3> gridPositions = new List<Vector3>();
        private bool isGridSelectionCreated;
        private bool isUpdate;
        [SerializeField]
        private TMP_Text explainConditionToContinuText;

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
            MusicPlateau.Instance.LaunchMusic(m_board.getMusic());
        }

        // Start is called before the first frame update
        void Start()
        {
            timePassing = 0;
            background.enabled = false;
            defeat.gameObject.SetActive(false);
            victory.gameObject.SetActive(false);
            rToMenu.enabled = false;
            rToMenuText.enabled = false;
            rToMenuCache.enabled = false;
            Base.Instance.SetHealthbar(healthbar);
            Base.Instance.SetHealthbarText(healthbarText);
            Base.Instance.UpdateHealth();
            Money.Instance.AddMoneyText(moneyText);
            Money.Instance.LaunchBoard();
            Player.Instance.SetDisplayTurret(m_board.getTurretDisplay());
            checkTime = 0;
            timerIsRunning = false;
            isUpdate = true;
            for(int i = 0; i < m_imageTurrets.Length; i++)
            {
                m_imageTurrets[i].enabled = false;
                m_imageBackground[i].enabled = false;
                m_imageCaches[i].enabled = false;
            }
            var tourelles = Player.Instance.GetTurrets();
            for (int i = 0; i < tourelles.Count; i++)
            {
                switch (tourelles[i].name)
                {
                    case "STAR": //TOURELLE DE LA STAR
                        m_imageTurrets[0].enabled = true;
                        m_imageBackground[0].enabled = true;
                        m_imageCaches[0].enabled = true;
                        break;
                    case "Grenadiere": //GRENADIERE
                        m_imageTurrets[1].enabled = true;
                        m_imageBackground[1].enabled = true;
                        m_imageCaches[1].enabled = true;
                        break;
                    case "Cryomancienne": //CRYOMANCIENNE
                        m_imageTurrets[2].enabled = true;
                        m_imageBackground[2].enabled = true;
                        m_imageCaches[2].enabled = true;
                        break;
                    case "Empoisonneuse": //EMPOISONNEUSE
                        m_imageTurrets[3].enabled = true;
                        m_imageBackground[3].enabled = true;
                        m_imageCaches[3].enabled = true;
                        break;
                    case "Pyromancienne": //PYROMANCIENNE
                        m_imageTurrets[4].enabled = true;
                        m_imageBackground[4].enabled = true;
                        m_imageCaches[4].enabled = true;
                        break;
                    case "Survolteuse": //SURVOLTEUSE
                        m_imageTurrets[5].enabled = true;
                        m_imageBackground[5].enabled = true;
                        m_imageCaches[5].enabled = true;
                        break;
                }
            }
        }


        public void endVictoryScreen()
        {
            GameManager.Instance().Victory(planet);
        }

        public void ReturnToMenu()
        {
            GameManager.Instance().ReturnToMenu();
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
            else if (Input.GetKeyDown("z"))
            {
                m_board.BeginningPoseTurret();
            }
            else if (Input.GetKeyDown("e"))
            {
                m_board.EndingPoseTurret();
            }
            else if (Input.GetKeyDown("r"))
            {
                TDManager.Instance().EndWave();
            }
            else if (Input.GetKeyDown("t"))
            {
                Money.Instance.AddMoney(500);
            }
            else if (Input.GetKeyDown("q"))
            {
                Spell.Instance.SetColldownTime(3);
            }
            else if (Input.GetKeyDown("s"))
            {
                Spell.Instance.SetSpellStunTime(3);
            }
            else if (Input.GetKeyDown("escape"))
            {
                if (timerIsRunning)
                {
                    ManageTime();
                    background.enabled = true;
                    rToMenu.enabled = true;
                    rToMenuText.enabled = true;
                    rToMenuCache.enabled = true;
                }
                else
                {
                    ManageTime();
                    background.enabled = false;
                    rToMenu.enabled = false;
                    rToMenuText.enabled = false;
                    rToMenuCache.enabled = false;
                }
            }

            //Si la condition de victoire et defaite n'est pas atteinte
            if (timerIsRunning && Base.Instance.IsAlive() && !TDManager.Instance().IsWaveEnd() && isUpdate)
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
            else if(!Base.Instance.IsAlive() && isUpdate)
            {
                background.enabled = true;
                defeat.gameObject.SetActive(true);
            }
            //Victoire
            else if(TDManager.Instance().IsWaveEnd() && isUpdate)
            {
                timerIsRunning = false;
                isUpdate = false;
                background.enabled = true;
                victory.gameObject.SetActive(true);
                MoneyForMerchant.Instance.AddMoney(150);
                SpecialEffectsHelper.Instance.victoryEffect(new Vector3(23, 1, 0));
                             
            }
        }
    }
}
