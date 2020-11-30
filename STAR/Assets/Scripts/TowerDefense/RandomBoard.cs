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

        private Planet planet;
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
        private int nbTiles;             // Size of the path
        private int currentTiles = 0;    // Number of tiles that composed the current path
        private int nbOffpath;           // Number of tiles that do not make progress toward the goal
        private int currentOffpath = 0;
        private int cpt = 0;

        public RandomBoard(Planet planet, int width = 12, int height = 18, int difficulty = 0, int seed = 0)
        {
            this.planet = planet;
            this.width = width;
            this.height = height;
            if (seed != 0)
                this.seed = seed;
            else
                this.seed = System.Environment.TickCount;
            this.difficulty = difficulty;

            switch (planet.size)
            {
                case Planet.Size.small:
                    nbTiles = 20 - planet.Rank;
                    break;
                case Planet.Size.medium:
                    nbTiles = 30 - planet.Rank * 2;
                    break;
                default:
                    nbTiles = 40 - planet.Rank * 3;
                    break;
            }
        }

        public void SetSeed(int seed)
        {
            this.seed = seed;
        }

        public int[,] RandomMat()
        {
            Random.InitState(seed);

            GenerateStartPoint();

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
                    if (rot == 0)
                        TurnTowardCenter();
                    else
                    {
                        if (rot == 1)
                            TurnLeft();
                        else
                            TurnRight();
                    }
                    cpt = 0;
                }
                else
                    ++cpt;

                if (!GoStraight())
                {
                    if (rot == 0)
                        isComplete = true;
                    else
                    {
                        ResetDir();
                        if (!GoStraight())
                            isComplete = true;
                    }
                }
            }

            return board;
        }

        private void GenerateStartPoint()
        {
            switch (planet.size)
            {
                case Planet.Size.small:
                    isSide = false;
                    break;
                case Planet.Size.medium:
                    int d2 = Random.Range(0, 2);
                    if (d2 == 0)
                        isSide = false;
                    else
                        isSide = true;
                    break;
                default:
                    isSide = true;
                    break;
            }

            (int, int) startPoint;
            if (isSide)
            {
                int startValue = Random.Range(1, height - 1);
                startPoint = (startValue, 0);
                dir = Dir.right;
                previousDir = Dir.right;
                nbOffpath = nbTiles - width;
            }
            else
            {
                int startValue = Random.Range(1, width - 1);
                startPoint = (0, startValue);
                dir = Dir.down;
                previousDir = Dir.down;
                nbOffpath = nbTiles - height;
            }

            boardBool = new bool[height, width];
            InitBoard();
            i = startPoint.Item1;
            j = startPoint.Item2;
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
            ++currentTiles;
            if (rot != 0)
                ++currentOffpath;
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

        private void TurnTowardCenter()
        {
            switch (dir)
            {
                case Dir.up:
                    if (j < width / 2)
                        TurnRight();
                    else
                        TurnLeft();
                    break;
                case Dir.right:
                    if (i < height / 2)
                        TurnRight();
                    else
                        TurnLeft();
                    break;
                case Dir.down:
                    if (j < width / 2)
                        TurnLeft();
                    else
                        TurnRight();
                    break;
                case Dir.left:
                    if (i < height / 2)
                        TurnLeft();
                    else
                        TurnRight();
                    break;
            }
        }

        private int CalculateThreshold()
        {
            if (currentTiles < 2)
                return 100;

            int th;
            int tilesLeft = nbTiles - currentTiles;
            int offpathLeft = nbOffpath - currentOffpath;
            int diff = tilesLeft - offpathLeft;

            if (rot != 0)
            {
                if (offpathLeft == 0)
                    th = 0;
                else if (diff == 0)
                    th = 100;
                else
                    th = 70 - (cpt * 5);
            }
            else
            {
                if (offpathLeft == 0)
                    th = 100;
                else if (diff == 0)
                    th = 0;
                else
                    th = 80 - (cpt * 5);
            }

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