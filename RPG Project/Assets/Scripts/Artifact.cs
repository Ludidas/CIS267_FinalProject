using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Artifact : MonoBehaviour
{
    public static int total;
    //when the player pick up the arm cannon and the upgraded sword it will set the dungeon completion to true
    public static void onpickup()
    {
        if (SceneManager.GetActiveScene().name == "DungeonCave")
        {
            GameManager.setDungeonCompletion(true, Dungeon.Cave);
        }
        if (SceneManager.GetActiveScene().name == "Dungeontwo")
        {
            GameManager.setDungeonCompletion(true, Dungeon.Ice);
        }
        if (SceneManager.GetActiveScene().name == "DungeonThree")
        {
            GameManager.setDungeonCompletion(true, Dungeon.Catacomb);
        }
    }
}
