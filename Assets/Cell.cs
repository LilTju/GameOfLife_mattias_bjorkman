using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool alive = false;
    public int aliveNeighbors;
    int evolutionState;


    SpriteRenderer spriteRenderer;

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = alive;

        if (alive)evolutionState++;
        else evolutionState = 0;
 
        aliveNeighbors = 0;
    }
}
