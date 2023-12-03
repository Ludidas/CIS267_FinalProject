using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCavePuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject[] memorizeGamePuzzle;
    [SerializeField] GameObject[] buttons;
    //Gates will go in the following order - 0 is bridge, 1 is block, both are associated. 2 is bridge, 3 is block, both are associated.
    //Keeping track of what has been placed in here
    //0 and 1 open to the memory puzzle
    [Header("Gates (Bridge First, Block Second)")]
    [SerializeField] GameObject[] gates;

    private int memoryPuzzleFinishes = 0;
    private int buttonPresses = 0;

    private void Start()
    {
        for (int i = 0; i < memorizeGamePuzzle.Length + buttons.Length; i = i + 2)
        {
            gates[i].SetActive(false);
            gates[i + 1].SetActive(true);
        }
    }

    public void openMemoryGate()
    {
        memoryPuzzleFinishes++;
        if (memoryPuzzleFinishes >= memorizeGamePuzzle.Length)
        {
            gates[0].SetActive(true);
            gates[1].SetActive(false);
        }
    }

    public void openButtonGate()
    {
        buttonPresses++;
        if (buttonPresses >= buttons.Length)
        {
            gates[2].SetActive(true);
            gates[3].SetActive(false);
        }
    }
}
