using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private static Transform player;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = (float).05;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y), speed));
    }
    public static void playerpostion(Transform p)
    {
        player = p;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            GameManager.changeHealth(-1);
            Destroy(this.gameObject);
            }
        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("UpgradedSword"))
        {
            Destroy(this.gameObject);
        }
        
        
    }
}
