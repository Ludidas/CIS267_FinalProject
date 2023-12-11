using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage;
    private bool isTouchingEnemy = false;
    private bool animStarted = false;

    public void startAnim()
    {
        animStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animStarted = false;
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            if (collision.gameObject.GetComponent<Enemy>().getHealth() <= 0)
            {
                collision.gameObject.GetComponent<Enemy>().onDeath();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (animStarted)
        {
            animStarted = false;
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
}
