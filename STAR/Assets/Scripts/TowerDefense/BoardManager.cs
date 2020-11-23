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
        [Serializable]
        public class Count
        {
            public int minimum;
            public int maximum;

            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }

        private static BoardManager instance;

        public int columns = 16;
        public int rows = 12;
        public Count objectCount = new Count(1, 3);
        public Count treeCount = new Count(1, 3);
        public Text round;
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
        private int m_roundMax;
        private int money;
        private int limitMoney;
        private int typeFloor;
        private int typePath;
        private int typeTree;
        private int typeFoliage;
        private int multiplicator;
        private List<Vector3> gridPositions = new List<Vector3>();


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

        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);
            return randomPosition;
        }

        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);
            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }

        void LayoutObjectAtRandom(GameObject[] tileArray, GameObject[] tileArray2, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);
            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject tileChoice2 = tileArray2[Random.Range(0, tileArray2.Length)];
                Vector3 scaleChange = new Vector3(0.6f, 0.6f, 0.2f);
                tileChoice2.transform.localScale = scaleChange;
                Instantiate(tileChoice2, randomPosition, Quaternion.identity);
                randomPosition.y--;
                randomPosition.z = randomPosition.z + 0.1f;
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);

            }
        }

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

        public int GetMoney()
        {
            return money;

        }

        public int[,] GetBoard()
        {
            return m_board.GetBoard();
        }
        public void SetMoney(int cost)
        {
            money -= cost;
            moneyText.text = ("" + money);
        }

        void InitiateBoard()
        {
            //m_board = new Board(18,12, planet);
            m_board.InitBoardAlone(planet);
            m_board.InitialiseList2();
            m_board.LayoutTree(1, 2);
            m_board.LayoutTree(9, 1);
            m_board.LayoutTree(12, 6);
            m_board.LayoutTree(11, 6);
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
            round.text = "Round " + m_round + " / " + m_roundMax;
            background.enabled = false;
            defeat.gameObject.SetActive(false);
            victory.gameObject.SetActive(false);
            m_base = new Base(healthbar, healthbarText);
            checkTime = 0;
            limitMoney = 10000;//2147483647
            money = 0;
            m_round = 1;
            m_roundMax = 4;
            timerIsRunning = false;
        }

    // Update is called once per frame
    void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("mouseDown = " + Input.mousePosition.x + "  || " + Input.mousePosition.y);
                // Debug.Log("x = " + Input.mousePosition.x / 2.56f + " Y = " + Input.mousePosition.y / 2.56f);
                //Instantiate(test_vert, new Vector3(Input.mousePosition.x / 2.56f, (Input.mousePosition.y / -2.56f) , -2f), Quaternion.identity);
            }

            if (Input.GetKeyDown("space"))
            {
                ManageTime();
            }
            else if (Input.GetKeyDown("a"))
            {
                m_base.ReceiveAttack(500);
            }
            else if(Input.GetKeyDown("z"))
            {
                m_board.TestPlacement();
            }
            else if(Input.GetKeyDown("e"))
            {
                m_board.TestPlacement2();
            }
            else if(Input.GetKeyDown("r"))
            {
                m_round++;
            }else if(Input.GetKeyDown("t"))
            {
                money += 500;
               if( money > limitMoney)
               {
                    money = limitMoney;
               }
            }

            if (timerIsRunning && m_base.IsAlive() && m_round <= m_roundMax)
            {
                timePassing += Time.deltaTime;
                if(checkTime < timePassing)
                {
                    checkTime++;
                    if(money < limitMoney)
                    {
                        money++;
                    }
                    moneyText.text = (""+ money);
                    round.text = "Round " + m_round + " / " + m_roundMax;
                    //Debug.Log("PV: " + m_base.GetLp());
                }
                DisplayTime(timePassing);
            }
            else if(!m_base.IsAlive())
            {
                background.enabled = true;
                defeat.gameObject.SetActive(true);
                            }
            else if(m_round >= m_roundMax)
            {
                background.enabled = true;
                victory.gameObject.SetActive(true);
            }
        }
    }
}
