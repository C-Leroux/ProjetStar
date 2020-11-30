using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TDManager : MonoBehaviour
    {
        private static TDManager instance;

        public Text waveText;
        private int[,] board;
        private float tileSize = 2.56f;
        private float padding = 0.64f;
        private Vector3 startPoint;
        private Vector3 endPoint;
        private List<Vector3> path;
        private WaveGenerator wg;
        private Wave wave;
        private Planet planet;
        private int waveMax;
        private bool debug = false;// debug

        private bool isActive = false;

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            path = new List<Vector3>();
            wave = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive)
            {
                if (wave == null || wave.IsCleared())
                {
                    Destroy(wave);
                    if ((wg.GetCurrentWave() + 1) <= wg.GetNbWaves())
                    {
                        waveText.text = "Round " + (wg.GetCurrentWave() + 1) + " / " + wg.GetNbWaves();
                    }
                    wave = wg.GenerateWave();
                }
            }
        }

        public static TDManager Instance()
        {
            return instance;
        }

        public void Init(int[,] newboard, Planet planet)
        {
            board = newboard;
            GetPath();
            Enemy.SetPath(path);
            this.planet = planet;
            wg = new WaveGenerator(planet, startPoint);
            isActive = true;
            waveText.text = "Round " + (wg.GetCurrentWave()+1) + " / " + wg.GetNbWaves();
        }

        public (int, int) GetStartPoint()
        {
            int x = -1;
            int y = -1;
            for (int i = 0; i < board.GetLength(0); ++i)
            {
                if (board[i, 0] == 11)
                {
                    x = i;
                    y = -1;
                }
            }
            for (int j = 0; j < board.GetLength(1); ++j)
            {
                if (board[0, j] == 10)
                {
                    x = -1;
                    y = j;
                }
            }
            startPoint = new Vector3(y * tileSize, -x * tileSize + padding, -0.5f);
            return (x, y);
        }

        public void GetPath()
        {
            (int, int) start = GetStartPoint();
            int ilast = start.Item1;
            int jlast = start.Item2;

            int i = ilast;
            int j = jlast;
            if (i == -1)
                ++i;
            else if (i == board.GetLength(0))
                --i;
            else if (j == -1)
                ++j;
            else
                --j;

            while (i >= 0 && i < board.GetLength(0) && j >= 0 && j < board.GetLength(1))
            {
                int curTile = board[i, j];
                switch(curTile)
                {
                    case 10:
                        if (i > ilast)
                        {
                            ++i;
                            ++ilast;
                        }
                        else
                        {
                            --i;
                            --ilast;
                        }
                        break;
                    case 11:
                        if (j > jlast)
                        {
                            ++j;
                            ++jlast;
                        }
                        else
                        {
                            --j;
                            --jlast;
                        }
                        break;
                    case 12:
                        AddTarget(i, j);
                        if (i > ilast)
                        {
                            ++j;
                            ++ilast;
                        }
                        else
                        {
                            --i;
                            --jlast;
                        }
                        break;
                    case 13:
                        AddTarget(i, j);
                        if (i < ilast)
                        {
                            --j;
                            --ilast;
                        }
                        else
                        {
                            ++i;
                            ++jlast;
                        }
                        break;
                    case 14:
                        AddTarget(i, j);
                        if (i > ilast)
                        {
                            --j;
                            ++ilast;
                        }
                        else
                        {
                            --i;
                            ++jlast;
                        }
                        break;
                    case 15:
                        AddTarget(i, j);
                        if (i < ilast)
                        {
                            ++j;
                            --ilast;
                        }
                        else
                        {
                            ++i;
                            --jlast;
                        }
                        break;
                    default:
                        break;
                }
            }
            AddTarget(ilast, jlast);
        }

        private void AddTarget(int i, int j)
        {
            path.Add(new Vector3(j * tileSize, -i * tileSize + padding, -0.5f));
        }

        //TEST
        public bool IsWaveEnd()
        {
            if(wg != null)
            {
                if (debug)
                {
                    return true;
                }
                if (wg.GetCurrentWave() > wg.GetNbWaves())
                {
                    return true;
                }
            }
            return false;
        }

        public void EndWave()
        {
            debug = true;
        }
    }
}