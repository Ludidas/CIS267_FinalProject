using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizeGame : MonoBehaviour
{
    private int[] orderOfPuzzle;
    private int gameStarted;

    private int step;
    private int stepInCurrentRound;
    private float timer;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Timer increases from 0
        timer = timer + Time.deltaTime;
        if (gameStarted == 1)
        {
            //If the timer is above the current step, switch to idle
            if (timer > step)
            {
                anim.SetInteger("CurProgress", 0);
            }
            //Move to different blinks depending on the timer and the step
            else if (timer > 3)
            {
                anim.SetInteger("CurProgress", orderOfPuzzle[3]);
            }
            else if (timer > 2)
            {
                anim.SetInteger("CurProgress", orderOfPuzzle[2]);
            }
            else if (timer > 1)
            {
                anim.SetInteger("CurProgress", orderOfPuzzle[1]);
            }
            else if (timer > 0)
            {
                anim.SetInteger("CurProgress", orderOfPuzzle[0]);
            }
            else if (timer > -1)
            {
                anim.SetInteger("CurProgress", 0);
            }
        }
    }

    //Generates the puzzle with a random order and no repeating numbers
    public void generatePuzzle()
    {
        //Declare orderOfPuzzle
        orderOfPuzzle = new int[4];

        //Loop four times
        for (int i = 0; i < 4; i++)
        {
            //Set orderOfPuzzle[i] to a random number between 1 and 4 inclusive
            orderOfPuzzle[i] = Random.Range(1, 5);

            //If the generated number matches an already made number, reset this loop
            for (int j = 0; j < i; j++)
            {
                if (orderOfPuzzle[i] == orderOfPuzzle[j])
                {
                    i--;
                }
            }
        }

        Debug.Log("Puzzle: " + orderOfPuzzle[0] + " " + orderOfPuzzle[1] + " " + orderOfPuzzle[2] + " " + orderOfPuzzle[3]);
    }

    //Set the animation to the first number in the order, and let that block blink.
    public void waitForGameToStart()
    {
        anim.SetInteger("CurProgress", orderOfPuzzle[0]);
    }

    public void blockClicked(float n)
    {
        //If the game has not started
        if (gameStarted == 0)
        {
            //Ensure that the player has clicked the first blinking block
            if (n == orderOfPuzzle[0])
            {
                //Start the game
                gameStarted = 1;
                //Move to the next step
                step++;
                //Set the timer to the step
                timer = -1;
            }
        }
        //If the game has started
        else if (gameStarted == 1 && timer > step)
        {
            //If the clicked block is the correct piece in the round
            if (n == orderOfPuzzle[stepInCurrentRound])
            {
                //Move to the next step
                stepInCurrentRound++;
                //If the player has clicked the correct amount of blocks
                if (stepInCurrentRound == step)
                {
                    //Reset the current round steps
                    stepInCurrentRound = 0;
                    //Move up one step
                    step++;
                    if (step > 4)
                    {
                        //Game is over
                        Debug.Log("PLAYER WIN");
                        gameStarted = -1;
                        anim.SetInteger("CurProgress", 5);
                        GetComponentInParent<DungeonCavePuzzleManager>().openMemoryGate();
                    }
                    else
                    {
                        //Set the timer to 0
                        timer = 0;
                    }
                }
            }
            else
            {
                //Resets the current round
                stepInCurrentRound = 0;
                //Plays an incorrect animation
                anim.SetTrigger("Incorrect");
                timer = -1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the collider hits the player
        if (collision.CompareTag("Player") && gameStarted != -1)
        {
            //Generate the puzzle and start waiting for the player
            generatePuzzle();
            gameStarted = 0;
            step = 1;
            stepInCurrentRound = 0;
            anim.SetInteger("CurProgress", orderOfPuzzle[0]);
        }
    }
}
