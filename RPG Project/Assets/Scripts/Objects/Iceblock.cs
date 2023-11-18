using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//NOTE FROM NICK
//Hi Chase, I wasn't 100% positive what this script was supposed to do, so I kinda just guessed.
//It straightens the ice blocks to prevent them from moving at an angle. The old code was very messy
//and confusing, and I'm not even really sure it worked. If this isn't what you wanted this script
//to do, let me know and feel free to fix it or revert it. BTW collisions with rigidbodies move the
//object already, so doing that in here is unnecessary.

public class Iceblock : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        straightenMovement();
    }

    private void straightenMovement()
    {
        //This script prevents the ice block from moving on an angle
        if (rb.velocity.x <= 0)
        {
            if (rb.velocity.x < -Mathf.Abs(rb.velocity.y))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            if (rb.velocity.x > Mathf.Abs(rb.velocity.y))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
}
