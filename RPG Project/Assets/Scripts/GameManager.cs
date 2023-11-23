using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static Vector2 spawnLocation = Vector2.zero;
    private static float health = 10f;
    private static float maxHealth = 10f;

    #region Spawn Locations

    //Sets the spawn location
    //Used in situations where the user will be moving scenes
    public static void setSpawnLocation(Vector2 l)
    {
        spawnLocation = l;
    }

    //Gets the spawn location
    public static Vector2 getSpawnLocation()
    {
        return spawnLocation;
    }

    #endregion

    #region Health

    //Gets the health of the player
    public static float getHealth()
    {
        return health;
    }

    //Changes the health of the player by h (positive adds, negative subtracts)
    public static void changeHealth(float h)
    {
        health = health + h;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public static float getMaxHealth()
    {
        return maxHealth;
    }

    public static void setMaxHealth(float mH)
    {
        maxHealth = mH;
    }

    #endregion
}
