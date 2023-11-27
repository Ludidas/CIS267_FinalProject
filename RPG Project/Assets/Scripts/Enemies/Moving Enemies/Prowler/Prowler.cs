using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Prowler : MonoBehaviour
{
    private Transform Player;
    private Rigidbody2D rb;
    private float jump;
    private float attack;
    private bool inranged;
    private float speed;
    private float cooldown;
    private float jumpcooldown;
    private float attackcooldown;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        inranged = false;
        speed = (float).07;
        cooldown = 6;
        jumpcooldown = 1;
        attackcooldown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (inranged && cooldown > 0)
        {
            rb.MovePosition(Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(Player.gameObject.transform.position.x, Player.gameObject.transform.position.y), speed));
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                jumpcooldown = 1;
            }
        }
        else if(inranged && cooldown <= 0)
        {
            rb.MovePosition(Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(Player.gameObject.transform.position.x, Player.gameObject.transform.position.y), (float).5));
            jumpcooldown -= Time.deltaTime;
            if(jumpcooldown <= 0)
            {
                cooldown = 2;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Player = collision.gameObject.transform;
            inranged = true;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject.transform;
            inranged = true;
            
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            if (attackcooldown <= 0)
            {
                Debug.Log("ProwlerHit");
                attackcooldown = (float).5;
            }
            else
            {
                attackcooldown -= Time.deltaTime;
            }
        }
    }

}
