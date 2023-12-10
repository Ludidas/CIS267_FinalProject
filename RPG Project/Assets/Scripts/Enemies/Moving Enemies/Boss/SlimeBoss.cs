using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
   
    [SerializeField] private float speed;

    private Transform player;
    private Transform playerspike;
    public Transform Spawner;
    public Transform Spawner2;
    public Transform Spawner3;
    public Transform Spikespawner;
    private Rigidbody2D rb;
    public GameObject slimeminion;
    public GameObject Spikess;
    private float cooldown;
    private bool inRange;
    private float attackCooldown;
    private float spawntimer;
    private float spikecooldown;


        void Start()
        {
             //use the slime contoller for the movement to kept it simple and due to deadlines. 
            cooldown = 0;
            inRange = false;
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            attackCooldown = 0;
            spawntimer = 5;
            spikecooldown = 11;

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
            Vector2 dir = player.position - Spikespawner.position;
            Spikespawner.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            if(spikecooldown <= 0)
            {
                Spike.playerpostion(playerspike);
                Instantiate(Spikess, Spikespawner.transform.position, Spikespawner.transform.rotation);
                spikecooldown = 10;
            }
            }
            if(spawntimer <= 0)
            {
            Instantiate(slimeminion, Spawner.transform.position, Spawner.transform.rotation);
            Instantiate(slimeminion, Spawner2.transform.position, Spawner2.transform.rotation);
            Instantiate(slimeminion, Spawner3.transform.position, Spawner3.transform.rotation);
            spawntimer = 5;
        }
            cooldown -= Time.deltaTime;
            spawntimer -= Time.deltaTime;
            spikecooldown -= Time.deltaTime;
           
            


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
                GameManager.changeHealth(-2);
                Debug.Log(GameManager.getHealth());
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
            if(spikecooldown > 0)
            {
                playerspike = player;
                
            }
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
