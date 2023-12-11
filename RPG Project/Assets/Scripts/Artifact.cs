using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Artifact : MonoBehaviour
{
   
    //when the player pick up the arm cannon and the upgraded sword it will set the dungeon completion to true
    public static void onpickup()
    {
        if (SceneManager.GetActiveScene().name == "DungeonCave")
        {
            GameManager.setDungeonCompletion(true, Dungeon.Cave);
            Debug.Log("Here");
        }
        if (SceneManager.GetActiveScene().name == "Dungeontwo")
        {
            GameManager.setDungeonCompletion(true, Dungeon.Ice);
            Debug.Log("Done");
        }
        if (SceneManager.GetActiveScene().name == "DungeonThree")
        {
            GameManager.setDungeonCompletion(true, Dungeon.Catacomb);
        }
    }
}
