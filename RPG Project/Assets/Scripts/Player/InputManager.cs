using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    [Header("Action Maps")]
    [SerializeField] private string[] listOfMaps;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    //Swaps between control maps based on the name provided. Returns true or false based on whether or not the map was successfully switched.
    public bool swapMap(string m)
    {
        bool inList = false;
        //Makes sure the map m exists in the listOfMaps
        for (int i = 0; i < listOfMaps.Length; i++)
        {
            if (listOfMaps[i] == m)
            {
                inList = true;
            }
        }
        //As long as the map exists, switch to it
        if (inList)
        {
            playerInput.SwitchCurrentActionMap(m);
            return true;
        }
        return false;
    }
}
