using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Board : MonoBehaviour
    {
        public GameObject spaceship;
        public GameObject test_vert;
        public GameObject[] floorTiles;
        public GameObject[] objectTiles;
        public GameObject[] treeTiles;
        public GameObject[] foliageTiles;
        public GameObject[] pathTiles;
        private Planet planet;
        private Case[,] gridCase;
        public int columns;
        public int rows;
        public turretDisplay turetDisp;
        public TurretBehaviour turretTemplate;
        private int multiplicator;
        private int typeFoliage;
        private Vector3[,] gridPositions2;
        private List<GameObject> gridPositions3;
        private int[,] board;
        private int typeFloor;
        private int typePath;
        private int typeTree;
        private Transform boardHolder;
        private string textTurret;
        private BoardManager boardManage;
        private bool isSelectionTurretModeActivate;
        private bool isStar;
        private bool isGrena;
        private bool isEmpoi;
        private bool isCryo;
        private bool isSUrvol;
        private bool isPyro;




        public Board(int x, int y)
        {
            columns = x;
            rows = y;
            gridCase = new Case[x, y];
        }

        public Board(int x, int y, Planet planet)
        {
            this.gridCase = new Case[x,y];
            this.planet = planet;
        }

        public void InitBoardAlone(Planet planet)
        {
            SetPlanet(planet);
            DefineTypePlanet();
            boardManage = BoardManager.Instance();
            textTurret = "";
            boardHolder = new GameObject("Contains").transform;
            gridCase = new Case[columns, rows];
            /*board = new int[,]
                {
                { 2 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 8 , 7 , 0 , 2 , 3 , 3 , 1 , 1 , 1 },
                { 2 , 0 , 12, 11, 11, 11, 11, 11, 13, 9 , 6 , 5 , 3 , 4 , 2 , 1 , 1 , 2 },
                { 2 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 9 , 7 , 4 , 2 , 3 , 2 , 2 , 1 , 2 },
                { 2 , 3 , 4 , 4 , 5 , 6 , 7 , 0 , 10, 0 , 4 , 3 , 1 , 0 , 0 , 1 , 2 , 3 },
                { 2 , 0 , 0 , 3 , 6 , 7 , 8 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 2 , 3 , 4 },
                { 4 , 2 , 1 , 2 , 9 , 8 , 8 , 0 , 12, 11, 11, 11, 11, 13, 0 , 1 , 1 , 0 },
                { 3 , 1 , 1 , 1 , 9 , 9 , 9 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 1 , 1 , 0 },
                { 2 , 3 , 2 , 1 , 9 , 8 , 9 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 2 , 0 , 0 },
                { 1 , 1 , 0 , 0 , 8 , 7 , 8 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 2 , 0 , 0 },
                { 1 , 2 , 4 , 5 , 7 , 6 , 7 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 2 , 2 , 1 },
                { 1 , 0 , 3 , 4 , 5 , 4 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 3 , 3 , 2 },
                { 0 , 0 , 1 , 5 , 4 , 3 , 0 , 0 , 0 , 15, 11, 11, 11, 14, 0 , 4 , 5 , 4 },
                { 0 , 0 , 2 , 3 , 2 , 2 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 5 , 6 , 7 }
                };*/
            RandomBoard randomBoard = new RandomBoard(planet, columns, rows);
            board = randomBoard.RandomMat();
            PrintBoard();
            typeFloor = (floorTiles.Length / 4) * multiplicator;
            typePath = (pathTiles.Length / 4) * multiplicator;
            typeTree = (treeTiles.Length / 4) * multiplicator;
            GameObject objectToLoad = floorTiles[0 + typeFloor];
            gridPositions3 = new List<GameObject>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (board[i, j] < 10)
                    {
                        objectToLoad = floorTiles[board[i, j] + typeFloor];
                    }
                    else
                    {
                        objectToLoad = pathTiles[board[i, j] + typePath - 10];
                    }
                    gridCase[j, i] = new Case(j, i, boardHolder);
                    gridCase[j, i].AddObject(objectToLoad);
                    if (i == 0 && board[i, j] == 10)
                    {
                        gridCase[j, i].SetDepart();
                    }
                    if (i == rows - 1 && board[i, j] == 10)
                    {
                        gridCase[j, i - 1].SetVillage();
                        gridCase[j, i - 1].AddObject(spaceship);
                    }
                }
            }
            turretTemplate = turetDisp.GetTurretBehaviour();

        }

        public void PrintBoard()
        {
            for (int i = 0; i < rows; ++i)
            {
                string str = "{";
                for (int j = 0; j < columns; ++j)
                    str += board[i, j] + ", ";
                print(str +"}");
            }    
        }


        public void AffObject()
        {
            for (int i = 0 ; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if(gridCase[i, j].GetTerrain() != null)
                    {
                       var terrain = Instantiate(gridCase[i, j].GetTerrain(), new Vector3(gridCase[i, j].GetX(), -gridCase[i, j].GetY(), -0.1f), Quaternion.identity) as GameObject;
                       terrain.transform.SetParent(boardHolder);
                       gridCase[i, j].SetTerrain(terrain);
                       //Destroy(gridCase[i, j].GetTerrain());//Working
                    }
                    if(gridCase[i, j].GetObject() != null)
                    {
                        double z = -0.3;
                        if (gridCase[i, j].GetObject().tag == "Trunk")
                        {
                            z = -0.2;
                        }
                        var new_object = Instantiate(gridCase[i, j].GetObject(), new Vector3(gridCase[i, j].GetX(), -gridCase[i, j].GetY(), (float)z), Quaternion.identity) as GameObject;
                        new_object.transform.SetParent(boardHolder);
                        gridCase[i, j].SetObject(new_object);
                    }
                   // gridCase[i,j].PrintAllObject();
                }
            }
        }


        public void InitialiseList2()
        {
            gridPositions2 = new Vector3[rows, columns];
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (board[y, x] < 10)
                    {
                        gridPositions2[y, x] = new Vector3(x * 2.56f, -y * 2.56f, -0.1f);
                    }
                    else
                    {
                        gridPositions2[y, x] = new Vector3(0, 0, 0);
                    }
                }
            }
        }

        public void setSelectionTurretMode(string name)
        {
            switch (name)
            {
                case "STAR": //TOURELLE DE LA STAR
                    isStar = !isStar;
                    isSelectionTurretModeActivate = isStar;
                    isGrena = false;
                    isCryo = false;
                    isEmpoi = false;
                    isPyro = false;
                    isSUrvol = false;
                    break;
                    
                case "Grenadiere": //GRENADIERE
                    isGrena = !isGrena;
                    isSelectionTurretModeActivate = isGrena;
                    isStar = false;
                    isCryo = false;
                    isEmpoi = false;
                    isPyro = false;
                    isSUrvol = false;
                    break;
                case "Cryomancienne": //CRYOMANCIENNE
                    isCryo = !isCryo;
                    isSelectionTurretModeActivate = isCryo;
                    isGrena = false;
                    isStar = false;
                    isEmpoi = false;
                    isPyro = false;
                    isSUrvol = false;
                    break;
                case "Empoisonneuse": //EMPOISONNEUSE
                    isEmpoi = !isEmpoi;
                    isSelectionTurretModeActivate = isEmpoi;
                    isGrena = false;
                    isCryo = false;
                    isStar = false;
                    isPyro = false;
                    isSUrvol = false;
                    break;
                case "Pyromancienne": //PYROMANCIENNE
                    isPyro = !isPyro;
                    isSelectionTurretModeActivate = isPyro;
                    isGrena = false;
                    isCryo = false;
                    isEmpoi = false;
                    isStar = false;
                    isSUrvol = false;
                    break;
                case "Survolteuse": //SURVOLTEUSE
                    isSUrvol = !isSUrvol;
                    isSelectionTurretModeActivate = isSUrvol;
                    isGrena = false;
                    isCryo = false;
                    isEmpoi = false;
                    isPyro = false;
                    isStar = false;
                    break;
            }        
            Debug.Log(isSelectionTurretModeActivate);
        }


        public bool GetSelectionTurretMode()
        {
            return isSelectionTurretModeActivate;
        }

        public string GetCurrentTurretName()
        {
            return textTurret;
        }
        
        public void BeginningPoseTurret() //Debut du mode posage de tourelle
        {
            if (gridPositions3.Count <= 0)
            {
                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        if (!gridPositions2[y, x].Equals(Vector3.zero))
                        {
                            GameObject gO = Instantiate(test_vert, gridPositions2[y, x], Quaternion.identity);
                            gridPositions3.Add(gO);
                            //gridCase[x, y].AddObject(gO, false);
                                                    
                        }
                    }
                }
            }
        }

        public void EndingPoseTurret() //Fin du mode posage de tourelle
        {
            for (int i = 0; i < gridPositions3.Count; i++)
            {
                Destroy(gridPositions3[i]);
            }
            gridPositions3 = new List<GameObject>();
        }

        public void LayoutTree(int x, int y)
        {
            if (!gridPositions2[y, x].Equals(Vector3.zero))
            {
                gridPositions2[y, x] = Vector3.zero;
                gridPositions2[y - 1, x] = Vector3.zero;
                GameObject tileChoice = treeTiles[Random.Range(typeTree, typeTree + 2)];
                //gridCase[x,y].AddObject(tileChoice, false);
                gridCase[x,y].AddObject(tileChoice);
                if (planet.biome != Planet.Biome.fire)
                {
                    GameObject tileChoice2 = foliageTiles[Random.Range(typeFoliage, typeFoliage + 1)];
                    Vector3 scaleChange = new Vector3(0.6f, 0.6f, 1f);
                    tileChoice2.transform.localScale = scaleChange;
                    //gridCase[x, y -1 ].AddObject(tileChoice2, false);
                    gridCase[x, y -1 ].AddObject(tileChoice2);
                }
            }

        }

        void DefineTypePlanet()
        {
            switch (planet.biome)
            {
                case Planet.Biome.ice:
                    multiplicator = 1;
                    typeFoliage = 2;
                    break;
                case Planet.Biome.fire:
                    multiplicator = 2;
                    break;
                case Planet.Biome.desert:
                    multiplicator = 3;
                    typeFoliage = 4;
                    break;
                default:
                    multiplicator = 0;
                    typeFoliage = 0;
                    break;
            }
        }

        void SetPlanet(Planet planet)
        {
            this.planet = planet;
        }

        public void SetStringTourelle(string text)
        {
            this.textTurret = text;
        }

        public void DisplayTurret()
        {
            if (textTurret != "")
            {
                Turret new_turret = turetDisp.GetTurret(textTurret);
                GameObject obj = GameObject.Instantiate(turretTemplate.gameObject);
                TurretBehaviour behaviour = obj.GetComponent<TurretBehaviour>();
                behaviour.init(new_turret);
                //var new_object = Instantiate(new_turret, new Vector3(gridCase[i, j].GetX(), -gridCase[i, j].GetY(), (float)z), Quaternion.identity) as GameObject;
                //new_object.transform.SetParent(boardHolder);
                //gridCase[i, j].SetObject(new_object);
                //turetDisp.createTurret(textTurret);
            }
        }
        public void DisplayTurret(float x, float y)
        {
            if (textTurret != "")
            {
                Turret new_turret = turetDisp.GetTurret(textTurret);
                int totalMoney = Money.Instance.GetCurrentMoney();
                int cost = new_turret.cost;
                if (totalMoney < cost)
                {
                    Debug.Log("Pas assez d'argent");
                }
                else
                {
                    //Debug.Log("x :" + x);
                    //Debug.Log("y :" + y);
                   for (int i = 0; i < columns; i++)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            if ((gridCase[i, j].GetX() == x) && (gridCase[i, j].GetY() == -y))
                            {
                                gridCase[i, j].SetTurret(new_turret);
                                GameObject obj = GameObject.Instantiate(turretTemplate.gameObject);
                                obj.transform.position = new Vector3(gridCase[i, j].GetX(), -gridCase[i, j].GetY(), -1);
                                TurretBehaviour behaviour = obj.GetComponent<TurretBehaviour>();
                                gridCase[i, j].SetTurretBehaviour(behaviour);
                                behaviour = gridCase[i, j].GetTurretBehaviour();
                                behaviour.init(new_turret);
                                gridCase[i, j].SetTurretBehaviour(null);
                                //var new_object = Instantiate(new_turret.sprite, new Vector3(gridCase[i, j].GetX(), -gridCase[i, j].GetY(), -1), Quaternion.identity) as GameObject;
                                Money.Instance.RemoveMoney(cost);
                            }
                        }
                    }
                    //turetDisp.createTurret(textTurret, x, y);
                }
            }
        }

        public int[,] GetBoard()
        {
            return board;
        }


        //Start is called before the first frame update
        void Start()
        {
            isSelectionTurretModeActivate = false;
            isStar = false;
            isGrena = false;
            isCryo = false;
            isEmpoi = false;
            isSUrvol = false;
            isPyro = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
