using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOfLife : MonoBehaviour
{
    public GameObject cellPrefab;
    Cell[,] cells;
    float cellSize = 0.25f; //Size of our cells
    int numberOfColums, numberOfRows;
    int spawnChancePercentage = 20;
    int outerLayer = 2;

    void Start()
    {
        //Lower framerate makes it easier to test and see whats happening.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 4;

        //Calculate our grid depending on size and cellSize
        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize *
            Camera.main.aspect * 2) / cellSize + outerLayer);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2
            / cellSize + outerLayer);

        //Initiate our matrix array
        cells = new Cell[numberOfColums, numberOfRows];

        //Create all objects

        //For each row
        for (int y = 1; y < numberOfRows - 1; y++)
        {
            //for each column in each row
            for (int x = 1; x < numberOfColums - 1; x++)
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
        for (int y = 1; y < numberOfRows - 1; y++)
        {
            for (int x = 1; x < numberOfColums - 1; x++)
            {
                if (cells[x, y].alive)
                {
                    if (cells[x - 1, y - 1] != null)
                        cells[x - 1, y - 1].aliveNeighbors++;

                    if (cells[x, y - 1] != null)
                        cells[x, y - 1].aliveNeighbors++;

                    if (cells[x + 1, y - 1] != null)
                        cells[x + 1, y - 1].aliveNeighbors++;

                    if (cells[x - 1, y] != null)
                        cells[x - 1, y].aliveNeighbors++;

                    if (cells[x, y + 1] != null)
                        cells[x, y + 1].aliveNeighbors++;

                    if (cells[x - 1, y + 1] != null)
                        cells[x - 1, y + 1].aliveNeighbors++;

                    if (cells[x, y + 1] != null)
                        cells[x, y + 1].aliveNeighbors++;

                    if (cells[x + 1, y + 1] != null)
                        cells[x + 1, y + 1].aliveNeighbors++;
                }
            }
        }

        //TODO: update buffer


        for (int y = 1; y < numberOfRows - 1; y++)
        {
            for (int x = 1; x < numberOfColums - 1; x++)
            {
                if (cells[x, y].alive)
                {
                    if (cells[x, y].aliveNeighbors < 2 || cells[x, y].aliveNeighbors > 3)
                        cells[x, y].alive = false;
                }
                else
                    if (cells[x, y].aliveNeighbors == 3)
                    cells[x, y].alive = true;

                cells[x, y].UpdateStatus();

                cells[x, y].aliveNeighbors = 0;
            }
        }
    }

    public void CheckNeighbors(Cell[,] cells, int x, int y)
    {
        if (cells[x - 1, y - 1] != null)
            cells[x - 1, y - 1].aliveNeighbors++;

        if (cells[x, y - 1] != null)
            cells[x, y - 1].aliveNeighbors++;

        if (cells[x + 1, y - 1] != null)
            cells[x + 1, y - 1].aliveNeighbors++;

        if (cells[x - 1, y] != null)
            cells[x - 1, y].aliveNeighbors++;

        if (cells[x, y + 1] != null)
            cells[x, y + 1].aliveNeighbors++;

        if (cells[x - 1, y + 1] != null)
            cells[x - 1, y + 1].aliveNeighbors++;

        if (cells[x, y + 1] != null)
            cells[x, y + 1].aliveNeighbors++;

        if (cells[x + 1, y + 1] != null)
            cells[x + 1, y + 1].aliveNeighbors++;
    }
}