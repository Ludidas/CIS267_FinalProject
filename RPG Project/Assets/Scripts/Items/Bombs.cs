using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isavtive;
    private CircleCollider2D circleCollider;
    public float timer;
    public float explosiontimer;
    private float cooldown;
    void Start()
    {
        timer = 3;
        explosiontimer = 1;
        circleCollider = GetComponent<CircleCollider2D>();
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
        
        if(isavtive)
        {
            if (timer <= 0)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                gameObject.tag = "Untagged";
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
