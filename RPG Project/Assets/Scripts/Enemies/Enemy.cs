using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private GameObject[] lootTable;

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
        int LootIndex;
        GameObject LootSpawned;
       
        LootIndex = Random.Range(0, lootTable.Length);
        LootSpawned = Instantiate(lootTable[LootIndex].gameObject);
        LootSpawned.transform.position = new Vector2(transform.position.x, transform.position.y);
        Destroy(this.gameObject.GetComponentInParent<SpriteRenderer>().gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.changeHealth(-damage);
        }
    }
}
