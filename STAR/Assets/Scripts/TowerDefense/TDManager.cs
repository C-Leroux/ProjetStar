﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TDManager : MonoBehaviour
    {
        private static TDManager instance;

        private int[,] board;
        private float tileSize = 2.56f;
        private float padding = 0.64f;
        private Vector3 startPoint;
        private Vector3 endPoint;
        private List<Vector3> path;
        private WaveGenerator wg;
        private Wave wave;
        private Planet planet;

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
            startPoint = new Vector3(2 * tileSize, -(-1) * tileSize + padding, -0.5f); // HARDCODED
            wg = new WaveGenerator(planet, startPoint);
            isActive = true;
        }

        public (int, int) GetStartPoint()
        {
            return (-1,2); // HARDCODED
        }

        public (int, int) GetEndPoint()
        {
            return (12, 9); // HARDCODED
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
    }
}