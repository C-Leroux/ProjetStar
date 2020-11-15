using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

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

    public int columns = 16;
    public int rows = 12;
    public Count objectCount = new Count(1, 3);
    public Count treeCount = new Count(1, 3);
    //public Count wallCount = new Count(5, 9);
    //public Count rockCount = new Count(4, 7);
    public GameObject spaceship;
    public GameObject[] floorTiles;
    public GameObject[] objectTiles;
    public GameObject[] treeTiles;
    public GameObject[] foliageTiles;
    public int[,] board;
    //public GameObject[] outerWallTiles;
    public GameObject[] pathTiles;
    private String type;
    private int typeFloor;
    private int typePath;
    private int typeTree;
    private int multiplicator;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();

        // for(int x = 1; x < columns -1; x++)
        for (int x = 0; x < columns - 1; x++)
        {
            //for (int y = 1; y < rows - 1; y++)
            for (int y = 0; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x * 2f, y * 2f, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        //for (int x = -1; x < columns + 1; x++)
        for (int x = 0; x < columns - 1; x++)
        {
            //for (int y = -1; y < rows + 1; y++)
            for (int y = 0; y < rows - 1; y++)
            {
                //GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                GameObject toInstantiate = floorTiles[0];
                /* if (x == -1 || x == columns || y == -1 || y == rows)
                     toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                */
                if (x == (columns / 2) - 1 && y > 3)
                {
                    toInstantiate = pathTiles[0];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x * 2f, y * 2f, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }


    }

    void TownSetup()
    {
        //boardHolder = new GameObject("Board").transform;
        GameObject toInstantiate = spaceship;
        Vector3 scaleChange = new Vector3(3f, 3f, 0);
        toInstantiate.transform.localScale = scaleChange;
        GameObject instance = Instantiate(toInstantiate, new Vector3(((columns / 2) - 1) * 2f, 1f * 2f, 0f), Quaternion.identity) as GameObject;
        //instance.transform.SetParent(boardHolder);
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

    void LayoutTree(int x, int y)
    {
        Debug.Log("Text: " + board[y, x]);
        if (board[y, x] < 10)
        {
            Debug.Log("Text: ");
            Vector3 position = new Vector3(x * 2.56f, -y * 2.56f, 1f);
            GameObject tileChoice = treeTiles[Random.Range(0, treeTiles.Length)];
            Instantiate(tileChoice, position, Quaternion.identity);
            position.y++;
            position.z = position.z - y* 0.1f;
            GameObject tileChoice2 = foliageTiles[Random.Range(0, foliageTiles.Length)];
            Vector3 scaleChange = new Vector3(0.6f, 0.6f, 1f);
            tileChoice2.transform.localScale = scaleChange;
            Instantiate(tileChoice2, position, Quaternion.identity);
        }

    }

    void ManuelSetup()
    {
        int x = 2;
        int y = 2;
        board = new int[,]
        {
            { 2 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 8 , 7 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            { 2 , 0 , 12, 11, 11, 11, 11, 11, 13, 9 , 6 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            { 2 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 9 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            { 2 , 3 , 4 , 4 , 5 , 6 , 7 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            { 2 , 0 , 0 , 0 , 6 , 7 , 8 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            { 4 , 2 , 0 , 0 , 9 , 8 , 8 , 0 , 12, 11, 11, 11, 11, 13, 0 , 0 , 0 , 0 },
            { 3 , 1 , 0 , 0 , 9 , 9 , 9 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 },
            { 2 , 3 , 0 , 0 , 9 , 8 , 9 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 },
            { 1 , 1 , 0 , 0 , 8 , 7 , 8 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 },
            { 1 , 2 , 4 , 5 , 7 , 6 , 7 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 },
            { 1 , 0 , 3 , 4 , 5 , 4 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 },
            { 0 , 0 , 1 , 5 , 4 , 3 , 0 , 0 , 0 , 15, 11, 11, 11, 14, 0 , 0 , 0 , 0 },
            { 0 , 0 , 2 , 3 , 2 , 2 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 }//,
            //{ 2 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            //{ 2 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 10, 0 , 0 , 0 , 0 , 0 , 0 , 0 },
           // { 2 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            //{ 2 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
           // { 2 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 }  
        };
        /*for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                board[i, j] = 10;
            }
        }*/


        AddToBoard2(board);
        /*GameObject floor1 = floorTiles[0 + typeR];
        GameObject floor2 = floorTiles[1 + typeR];
        GameObject floor3 = floorTiles[2 + typeR];
        GameObject floor4 = floorTiles[3 + typeR];
        GameObject path1 = pathTiles[0];
        GameObject path2 = pathTiles[1];
        GameObject path3 = pathTiles[2];
        GameObject path4 = pathTiles[3];
        GameObject skull = objectTiles[0];*/
        /*GameObject instance = Instantiate(floor3, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        GameObject instance2 = Instantiate(floor1, new Vector3(x * 1f, 0f, 0f), Quaternion.identity) as GameObject;
        GameObject instance3 = Instantiate(floor1, new Vector3(0f,  y * -1f, 0f), Quaternion.identity) as GameObject;
        GameObject instance4 = Instantiate(path1, new Vector3(x * 2f, 0f, 0f), Quaternion.identity) as GameObject;*/


        /*AddToBoard(floor3, 0,0,0);
        AddToBoard(skull, 1,0,1);
        AddToBoard(floor1, 1,0,0);
        AddToBoard(floor1, 0,-1,0);
        AddToBoard(path1, 2,0,0);
        AddToBoard(floor1, 3,0,0);
        AddToBoard(floor1, 4,0,0);
        AddToBoard(floor1, 5,0,0);
        AddToBoard(floor1, 6,0,0);
        AddToBoard(path3, 2,-1,0);
        AddToBoard(floor1, 1,-1,0);
        AddToBoard(path2, 3,-1,0);
        AddToBoard(path2, 4,-1,0);
        AddToBoard(path2, 5,-1,0);
        AddToBoard(path4, 6,-1,0);
        AddToBoard(path1, 6,-2,0);
        AddToBoard(path1, 6,-3,0);
        AddToBoard(path1, 6,-4,0);
        AddToBoard(path1, 6,-5,0);
        AddToBoard(path1, 6,-6,0);
        AddToBoard(path1, 6,-7,0);*/
    }

    void AddToBoard(GameObject objectToLoad, int x, int y, int z)
    {
        GameObject instance = Instantiate(objectToLoad, new Vector3(x * 2.56f, y * 2.56f, z * 1f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    void AddToBoard2(int[,] board)
    {
        GameObject objectToLoad = floorTiles[0 + typeFloor];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if(board[i,j] < 10)
                {
                    objectToLoad = floorTiles[board[i,j] + typeFloor];           
                }
                else
                {
                    objectToLoad = pathTiles[board[i,j] + typePath - 10];
                }
                GameObject instance = Instantiate(objectToLoad, new Vector3(j * 2.56f, -i * 2.56f, 0 * 1f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    void InitiateBoard()
    {
        boardHolder = new GameObject("Board").transform;
        type = "forest";
        switch (type)
        {
            case "ice":
                multiplicator = 1;
                break;
            case "lava":
                multiplicator = 2;
                break;
            case "sand":
                multiplicator = 3;                    ;
                break;
            default:
                multiplicator = 0;
                break;
        }
        typeFloor = (floorTiles.Length /4) * multiplicator;
        Debug.Log(typeFloor);
        typePath = (pathTiles.Length / 4) * multiplicator; ;
        typeTree = (treeTiles.Length / 4) * multiplicator; ;
    }

    public void SetupScene(int level)
    {
        InitiateBoard();
        //BoardSetup();
        //InitialiseList();
        ////LayoutObjectAtRandom(objectTiles, objectCount.minimum, objectCount.maximum);
        //LayoutObjectAtRandom(treeTiles, foliageTiles, treeCount.minimum, treeCount.maximum);
        //TownSetup();
        ManuelSetup();
        LayoutTree(1, 2);
        LayoutTree(9, 1);
        LayoutTree(12, 6);
        LayoutTree(11, 6);
        LayoutTree(10, 6);
        LayoutTree(7, 6);
        LayoutTree(9, 8);
        LayoutTree(10, 9);
        LayoutTree(11, 7);
        LayoutTree(3, 8);
        LayoutTree(12, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
