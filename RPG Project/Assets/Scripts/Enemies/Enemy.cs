using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private GameObject[] lootTable;

    private float damageCooldown = 0f;

    private void Update()
    {
        damageCooldown -= Time.deltaTime;
    }

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCooldown <= 0f)
            {
                GameManager.changeHealth(-damage);
                damageCooldown = 0.5f;
            }
        }
    }
}
