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
            board = new int[,]
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
                };
            typeFloor = (floorTiles.Length / 4) * multiplicator;
            typePath = (pathTiles.Length / 4) * multiplicator;
            typeTree = (treeTiles.Length / 4) * multiplicator;
            GameObject objectToLoad = floorTiles[0 + typeFloor];
            bool verif;
            gridPositions3 = new List<GameObject>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (board[i, j] < 10)
                    {
                        objectToLoad = floorTiles[board[i, j] + typeFloor];
                        verif = true;
                    }
                    else
                    {
                        objectToLoad = pathTiles[board[i, j] + typePath - 10];
                        verif = false;
                    }
                    gridCase[j, i] = new Case(j, i, boardHolder);
                    gridCase[j, i].AddObject(objectToLoad, verif);
                    if (i == 0 && board[i, j] == 10)
                    {
                        gridCase[j, i].SetDepart();
                        //gridCase[j, i].AddObject(test_vert, verif);
                    }
                    if (i == rows - 1 && board[i, j] == 10)
                    {
                        gridCase[j, i - 1].SetVillage();
                        gridCase[j, i - 1].AddObject(spaceship, verif);
                    }
                }
            }

        }


        public void AffObject()
        {
            for (int i = 0 ; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    gridCase[i,j].PrintAllObject();
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



        public void setSelectionTurretMode()
        {
            isSelectionTurretModeActivate = !isSelectionTurretModeActivate;
            Debug.Log(isSelectionTurretModeActivate);
        }

        public bool getSelectionTurretMode()
        {
            return isSelectionTurretModeActivate;
        }

        public string getCurrentTurretName()
        {
            return textTurret;
        }
        public Sprite getCurrentTurretSprite(string turretName)
        {
            return turetDisp.getSprite(turretName);
        }
        
        public void TestPlacement() //Debut du mode posage de tourelle
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

        public void TestPlacement2() //Fin du mode posage de tourelle
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
                gridPositions2[y + 1, x] = Vector3.zero;
                GameObject tileChoice = treeTiles[Random.Range(typeTree, typeTree + 2)];
                gridCase[x,y].AddObject(tileChoice, false);
                if (planet.biome != Planet.Biome.fire)
                {
                    GameObject tileChoice2 = foliageTiles[Random.Range(typeFoliage, typeFoliage + 1)];
                    Vector3 scaleChange = new Vector3(0.6f, 0.6f, 1f);
                    tileChoice2.transform.localScale = scaleChange;
                    gridCase[x, y -1 ].AddObject(tileChoice2, false);
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

        public void setStringTourelle(string text)
        {
            this.textTurret = text;
        }

        public void afficheTourelle()
        {
            if (textTurret != "")
            {
                turetDisp.createTurret(textTurret);
            }
        }
        public void afficheTourelle(float x, float y)
        {
            if (textTurret != "")
            {
                int totalMoney = boardManage.GetMoney();
                int cost = turetDisp.getCout(textTurret);
                if (totalMoney < cost)
                {
                    Debug.Log("Pas assez d'argent");
                }
                else
                {
                   /* for (int i = 0; i < columns; i++)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            if ((gridCase[i, j].GetX() == x) && (gridCase[i, j].GetY() == y))
                            {
                                Debug.Log(" i" + i + " j" + j);
                            }
                        }
                    }*/
                    turetDisp.createTurret(textTurret, x, y);
                    boardManage.SetMoney(cost);
                }
            }
        }

        public int[,] GetBoard()
        {
            return board;
        }

        void Parent(GameObject childOb)
        {
            childOb.transform.parent = boardHolder.transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            isSelectionTurretModeActivate = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
