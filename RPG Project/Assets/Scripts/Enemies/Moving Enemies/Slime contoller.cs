using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slimecontoller : MonoBehaviour
{
    private Transform player;
    private Enemy health;
    private Rigidbody2D rb;
    private float cooldown;
    private float cooldown2;
    private float speed;
    private bool inranged;
    private float attackcooldown;
    void Start()
    {
        cooldown = 2;
        cooldown2 = 2;
        speed = (float).03;
        inranged = false;
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Enemy>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        attackcooldown = 2;
        health.setHealth(2);

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)     
        {
            if(cooldown2 <= 0)
            {
                cooldown2 = 2;
            }
            move();
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
        
        
    }
    private void move()
    {
        if (inranged)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.MovePosition(Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y), speed));
            cooldown2 -= Time.deltaTime;
            if(cooldown2 <= 0)
            {
                cooldown = 2;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
               
            }
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            health.takeDamage(5);
            if (health.getHealth() <= 0)
            {
                health.onDeath();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        


         if (collision.gameObject.CompareTag("Player"))
         {
                player = collision.gameObject.transform;
                inranged = true;
         }
         if (collision.gameObject.CompareTag("Sword"))
         {
            health.takeDamage(1);
         if (health.getHealth() <= 0)
         {
                health.onDeath();
         }
         }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attackcooldown <= 0)
            {
                Debug.Log("SlimeHit");
                attackcooldown = 1;
            }
            else
            {
                attackcooldown -= Time.deltaTime;
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
                player = collision.gameObject.transform;
                inranged = true;
            
        }
           
        
     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inranged = false;
    }
}
