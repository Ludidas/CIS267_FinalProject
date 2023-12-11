using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpot : MonoBehaviour
{

    // Start is called before the first frame update
    public static bool active;
    void Start()
    {
        if (active)
        {
            Renderer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Renderer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }
    public static void Active(bool s)
    {
        active = s;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active)
        {
            if(GameManager.getMaxHealth() >= 10)
            {
                GameManager.changeHealth(2);
                Active(false);
            }
            Destroy(this.gameObject);
               
        }
    }

}
