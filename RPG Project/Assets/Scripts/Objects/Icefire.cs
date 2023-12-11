using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Icefire : MonoBehaviour
{
    private float x;
    private float y;
    public Transform player;
    public GameObject ib;
    private Iceblock block;
    private Rigidbody2D rb;
    public float move;
    private bool inrange;
    // Start is called before the first frame update
    void Start()
    {
        move = (float).1;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        block = ib.gameObject.GetComponent<Iceblock>();
        inrange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inrange)
        {
            rb.MovePosition(Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y), move));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.transform;
            inrange = true;
        }
        if(collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("UpgradedSword"))
        {
            Instantiate(ib, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player  = collision.gameObject.transform;
            inrange = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.changeHealth(-1);
            Debug.Log(GameManager.getHealth());
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
