using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Sprite miniLife;
    public Sprite gigaLife;
    public Sprite dyingLife;

    public bool alive = false;
    public int aliveNeighbors;
    int evolutionState;
    bool hasGracePeriod;


    SpriteRenderer spriteRenderer;

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = alive;

        if (alive)
        {
            evolutionState++;
            hasGracePeriod = true;
        }
        else
            evolutionState = 0;

        if (evolutionState > 1)
            spriteRenderer.sprite = gigaLife;

        else if (alive && evolutionState == 1)
            spriteRenderer.sprite = miniLife;

        else if (!alive && hasGracePeriod)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = dyingLife;
            hasGracePeriod = false;
        }

        



        aliveNeighbors = 0;
    }
}
