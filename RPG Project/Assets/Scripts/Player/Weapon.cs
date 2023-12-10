using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            if (collision.gameObject.GetComponent<Enemy>().getHealth() <= 0)
            {
                collision.gameObject.GetComponent<Enemy>().onDeath();
            }
        }
    }
}
