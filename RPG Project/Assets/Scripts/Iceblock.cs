using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Iceblock : MonoBehaviour
{
    private Rigidbody2D rb;
    private float startposx;
    private float startposy;
    private bool yaxis;
    
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();  
       startposx = transform.position.x;
        startposy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if(collision.gameObject.CompareTag("Wall"))
        {
           
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.x > startposx && yaxis == false)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.left;
            }
            else if (collision.gameObject.transform.position.x < startposx && yaxis == false)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = Vector2.right;
            }
            else if(collision.gameObject.transform.position.y > startposy && yaxis == true)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.down;
            }
            else if(collision.gameObject.transform.position.y < startposy && yaxis == true)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.up;
            }
        }
       
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            yaxis = true;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        yaxis= false;
    }
}
