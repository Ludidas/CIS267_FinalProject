using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static Vector2 spawnLocation = Vector2.zero;

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
}
