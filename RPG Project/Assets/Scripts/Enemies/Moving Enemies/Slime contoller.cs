using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slimecontoller : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform player;
    private Rigidbody2D rb;
    private float cooldown;
    private bool inRange;
    private float attackCooldown;

    void Start()
    {
        cooldown = 0;
        inRange = false;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        attackCooldown = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the player is in range
        if (inRange)
        {
            //Ensure the jump cooldown is less than 0
            if (cooldown <= 0)
            {
                //Set the cooldown
                cooldown = 3f;
                //Set the velocity to move towards the player, using the linear drag to slow down
                rb.velocity = new Vector2(speed * -Mathf.Sin((transform.position.x - player.position.x) / Mathf.Sqrt(Mathf.Pow(transform.position.y - player.position.y, 2) + Mathf.Pow(transform.position.x - player.position.x, 2))),
                    speed * -Mathf.Sin((transform.position.y - player.position.y) / Mathf.Sqrt(Mathf.Pow(transform.position.y - player.position.y, 2) + Mathf.Pow(transform.position.x - player.position.x, 2))));
            }
        }
        //Reduce the cooldown by Time.deltaTime
        cooldown -= Time.deltaTime;


        //if (cooldown <= 0)     
        //{
        //    if(cooldown2 <= 0)
        //    {
        //        cooldown2 = 2;
        //    }
        //    move();
        //}
        //else
        //{
        //    cooldown -= Time.deltaTime;
        //}
        //health.getHealth();

    }

    private void move()
    {
        //if (inranged)
        //{
        //    rb.MovePosition(Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y), speed));
        //    cooldown2 -= Time.deltaTime;
        //    if(cooldown2 <= 0)
        //    {
        //        cooldown = 2;
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player's collider hits this trigger
        if (collision.gameObject.CompareTag("Player"))
        {
            //Get the position
            player = collision.gameObject.transform;
            //Set inRange to true
            inRange = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //When the player gets hit, create a cooldown as long as the player is being hit before the player takes more damage
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attackCooldown <= 0)
            {
                Debug.Log("SlimeHit");
                attackCooldown = 1;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Create a live update of the player's position
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.transform;
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Set inRange to false
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Reset the attackcooldown
        if (collision.gameObject.CompareTag("Player"))
        {
            attackCooldown = 0;
        }

    }
}
