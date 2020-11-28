using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RandomBoard
    {
        private enum Dir
        {
            up,
            right,
            down,
            left
        }

        private int seed;
        private int difficulty;         // 0 = easy, 1 = medium, 2 = hard
        private int height;
        private int width;

        private bool isSide = true;     // True if the path connect the left to the right, false otherwise
        private Dir dir;                // Current direction faced
        private Dir previousDir;        // Previous direction
        private int rot;                // -1 = left, 0 = straight, 1 = right
        private int i;                  // height cursor
        private int j;                  // width cursor
        private bool[,] boardBool;
        private int[,] board;
        private bool isComplete = false;
        private int nbTiles = 0;        // Number of tiles that composed the current path

        public RandomBoard(int width = 12, int height = 18, int seed = 0, int difficulty = 0)
        {
            this.width = width;
            this.height = height;
            if (seed != 0)
                this.seed = seed;
            else
                this.seed = System.Environment.TickCount;
            this.difficulty = difficulty;
        }

        public void SetSeed(int seed)
        {
            this.seed = seed;
        }

        public int[,] RandomMat()
        {
            Random.InitState(seed);

            int startValue = Random.Range(0, height + width);
            (int, int) startPoint;
            if (startValue < height)
            {
                startPoint = (startValue, 0);
                dir = Dir.right;
                previousDir = Dir.right;
                isSide = true;
            }
            else
            {
                startPoint = (0, startValue - height);
                dir = Dir.down;
                previousDir = Dir.down;
                isSide = false;
            }

            boardBool = new bool[height, width];
            InitBoard();
            i = startPoint.Item1;
            j = startPoint.Item2;
            boardBool[i, j] = true;
            if (isSide)
                board[i, j] = 11;
            else
                board[i, j] = 10;

            while(!isComplete)
            {
                int d100 = Random.Range(0, 100);
                int threshold = CalculateThreshold();

                if (d100 >= threshold)
                {
                    if (d100 < (100 + threshold) / 2)
                        TurnLeft();
                    else
                        TurnRight();
                }

                if (!GoStraight())
                {
                    if (rot == 0)
                        isComplete = true;
                    else
                        ResetDir();
                }
            }

            return board;
        }

        private void InitBoard()
        {
            board = new int[height, width];
            for (int x = 0; x < height; ++x)
            {
                for (int y = 0; y < width; ++y)
                {
                    board[x, y] = Random.Range(0, 10);
                }
            }
        }

        private bool GoStraight()
        {
            switch (dir)
            {
                case Dir.up:
                    --i;
                    if (i == -1)
                    {
                        ++i;
                        return false;
                    }
                    break;
                case Dir.right:
                    ++j;
                    if (j == width)
                    {
                        --j;
                        return false;
                    }
                    break;
                case Dir.down:
                    ++i;
                    if (i == height)
                    {
                        --i;
                        return false;
                    }
                    break;
                case Dir.left:
                    --j;
                    if (j == -1)
                    {
                        ++j;
                        return false;
                    }
                    break;
            }
            ++nbTiles;
            boardBool[i, j] = true;
            SetTileStraight();
            previousDir = dir;

            return true;
        }

        private void TurnLeft()
        {
            if (rot == -1)
                return;
            --rot;
            --dir;
            if ((int)dir == -1)
                dir = Dir.left;

            if (dir != previousDir)
            {
                switch (dir)
                {
                    case Dir.up:
                        board[i, j] = 14;
                        break;
                    case Dir.right:
                        board[i, j] = 12;
                        break;
                    case Dir.down:
                        board[i, j] = 15;
                        break;
                    case Dir.left:
                        board[i, j] = 13;
                        break;
                }
            }
            else
            {
                SetTileStraight();
            }
        }

        private void TurnRight()
        {
            if (rot == 1)
                return;
            ++rot;
            ++dir;
            if ((int)dir == 4)
                dir = Dir.up;

            if (dir != previousDir)
            {
                switch (dir)
                {
                    case Dir.up:
                        board[i, j] = 12;
                        break;
                    case Dir.right:
                        board[i, j] = 15;
                        break;
                    case Dir.down:
                        board[i, j] = 13;
                        break;
                    case Dir.left:
                        board[i, j] = 14;
                        break;
                }
            }
            else
            {
                SetTileStraight();
            }
        }

        private void ResetDir()
        {
            if (rot == -1)
                TurnRight();
            else if (rot == 1)
                TurnLeft();
        }

        private int CalculateThreshold()
        {
            int th = 70 + 10 * difficulty;

            if (rot != 0)
                th /= 2;
            else
                th += nbTiles / 2;

            return th;
        }

        private void SetTileStraight()
        {
            if (dir == Dir.left || dir == Dir.right)
                board[i, j] = 11;
            else
                board[i, j] = 10;
        }
    }
}