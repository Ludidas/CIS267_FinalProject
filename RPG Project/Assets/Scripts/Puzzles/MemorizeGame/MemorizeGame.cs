using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizeGame : MonoBehaviour
{
    private int[] orderOfPuzzle;

    private int step;
    private int stepInCurrentMove;
    private int stepInDisplayingMoves;
    private float timer;
    private bool displayingMoves;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timer = 1f;
        stepInDisplayingMoves = 0;
        displayingMoves = false;
    }

    private void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0f)
        {
            timerAlert();
            timer = 1f;
        }
    }

    public void generatePuzzle()
    {
        step = 0;
        orderOfPuzzle = new int[4];

        for (int i = 0; i < 4; i++)
        {
            orderOfPuzzle[i] = Random.Range(0, 4);
            
            for (int j = 0; j < i; j++)
            {
                if (orderOfPuzzle[i] == orderOfPuzzle[j])
                {
                    i--;
                }
            }
        }

        Debug.Log("Puzzle: " + orderOfPuzzle[0] + " " + orderOfPuzzle[1] + " " + orderOfPuzzle[2] + " " + orderOfPuzzle[3]);

        idle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            generatePuzzle();
        }
    }

    private void idle()
    {
        anim.SetInteger("CurProgress", orderOfPuzzle[0] + 1);
    }

    private void timerAlert()
    {
        if (displayingMoves)
        {
            displayMoves();
        }
    }

    public void blockClicked(float num)
    {
        Debug.Log("Block Clicked " + num);
        if (orderOfPuzzle[stepInCurrentMove] == num)
        {
            stepInCurrentMove++;
            if (stepInCurrentMove > step)
            {
                step++;
                timer = 1f;
                displayingMoves = true;
            }
        }
    }

    private void displayMoves()
    {
        anim.SetInteger("CurProgress", orderOfPuzzle[stepInDisplayingMoves] + 1);
        stepInDisplayingMoves++;
        if (stepInDisplayingMoves > step)
        {
            displayingMoves = false;
        }
    }
}
