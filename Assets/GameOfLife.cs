using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public GameObject cellPrefab;
    Cell[,] cells;
    Cell[,] nextCells;
    int aliveNeighbors;
    float cellSize = 0.25f; //Size of our cells
    int numberOfColums, numberOfRows;
    int spawnChancePercentage = 5;

    void Start()
    {
        //Lower framerate makes it easier to test and see whats happening.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 4;

        //Calculate our grid depending on size and cellSize
        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize *
            Camera.main.aspect * 2) / cellSize);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2 / cellSize);

        //Initiate our matrix array
        cells = new Cell[numberOfColums, numberOfRows];
        nextCells = new Cell[numberOfColums, numberOfRows];

        //Create all objects

        //For each row
        for (int y = 0; y < numberOfRows; y++)
        {
            //for each column in each row
            for (int x = 0; x < numberOfColums; x++)
            {
                //Create our game cell objects, multiply by cellSize for correct world placement
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize *
                    Camera.main.aspect,
                    y * cellSize - Camera.main.orthographicSize);

                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<Cell>();

                //Random check to see if it should be alive
                if (Random.Range(0, 100) < spawnChancePercentage)
                {
                    cells[x, y].alive = true;
                }

                cells[x, y].UpdateStatus();
            }
        }
    }


    void Update()
    {
        //TODO: Calculate next generation
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                if (cells[x, y].alive)
                {
                    aliveNeighbors = CheckNeighbors(cells, x, y);

                    if (aliveNeighbors < 2 || aliveNeighbors > 3)
                        nextCells[x, y].alive = false;
                    else
                        nextCells[x, y].alive = true;
                }

                if (!cells[x, y].alive)
                {
                    aliveNeighbors = CheckNeighbors(cells, x, y);

                    if (aliveNeighbors == 3)
                        nextCells[x, y].alive = true;
                }
            }
        }

        //TODO: update buffer
        cells = nextCells;


        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; x++)
            {
                cells[x, y].UpdateStatus();
            }
        }
    }

    public int CheckNeighbors(Cell[,] cells, int x, int y)
    {
        int aliveNeighbors = 0;

        if (cells[x - 1, y - 1].alive)
            aliveNeighbors++;
        if (cells[x, y - 1].alive)
            aliveNeighbors++;
        if (cells[x + 1, y - 1].alive)
            aliveNeighbors++;
        if (cells[x - 1, y].alive)
            aliveNeighbors++;
        if (cells[x, y + 1].alive)
            aliveNeighbors++;
        if (cells[x - 1, y + 1].alive)
            aliveNeighbors++;
        if (cells[x, y + 1].alive)
            aliveNeighbors++;
        if (cells[x + 1, y + 1].alive)
            aliveNeighbors++;

        return aliveNeighbors;
    }
}