using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isavtive;
    private CircleCollider2D circleCollider;
    public float timer;
    public float explosiontimer;
    private float cooldown;

    private Animator bombAnimator;
    void Start()
    {
        timer = 3;
        explosiontimer = 1;
        circleCollider = GetComponent<CircleCollider2D>();
        bombAnimator = GetComponent<Animator>();
        cooldown = 0;
        gameObject.tag = "Bomb";
    }

    // Update is called once per frame
    void Update()
    {
        explosion();
    }
    public static void setavtive(bool s)
    {
        isavtive=s;
    }
    private void explosion()
    {
        
        if(this.gameObject.CompareTag("Untagged"))
        {
            bombAnimator.SetBool("blowingUp", true);

            if (timer <= 0)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                
                if(explosiontimer <= 0)
                {
                    isavtive = false;
                    Destroy(this.gameObject);

                }
                else
                {
                    explosiontimer -= Time.deltaTime;
                }
                
            }
            else
            {
                {
                    timer -= Time.deltaTime;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (isavtive)
            {
                this.gameObject.tag = "Untagged";
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isavtive)
            {
                this.gameObject.tag = "Untagged";
                setavtive(false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (cooldown == 0) 
            {
                GameManager.changeHealth(-2);
                Debug.Log(GameManager.getHealth());
                cooldown = 2;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (cooldown == 0)
            {
                GameManager.changeHealth(-2);
                Debug.Log(GameManager.getHealth());
                cooldown = 2;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}
