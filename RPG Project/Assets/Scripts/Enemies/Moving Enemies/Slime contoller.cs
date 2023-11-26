using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slimecontoller : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private float cooldown;
    private float cooldown2;
    private float cooldown3;
    private float speed;
    private bool inranged;
    void Start()
    {
        cooldown = 2;
        cooldown2 = 2;
        cooldown3 = 2;
        speed = (float).05;
        inranged = false;
        rb = GetComponent<Rigidbody2D>();
       
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
            rb.MovePosition(Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y), speed));
            cooldown2 -= Time.deltaTime;
            if(cooldown2 <= 0)
            {
                cooldown = 2;
            }
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (cooldown3 <= 0)
        {


            if (collision.gameObject.CompareTag("Player"))
            {
                player = collision.gameObject.transform;
                inranged = true;
            }
        }
        else
        {
            cooldown3 -= Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (cooldown3 <= 0)
        {


            if (collision.gameObject.CompareTag("Player"))
            {
                player = collision.gameObject.transform;
                inranged = true;
            }
        }
        else
        {
            cooldown3 -= Time.deltaTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inranged = false;
    }
}
