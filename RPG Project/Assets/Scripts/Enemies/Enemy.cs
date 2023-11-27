using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private string[] lootTable;

    public void takeDamage(float d)
    {
        health = health - d;
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float h)
    {
        health = h;
    }

    public void onDeath()
    {
        for (int i = 0; i < lootTable.Length; i++)
        {
            //Instantiate each item to be picked up
        }
    }
}
