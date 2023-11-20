using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttoncontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Door;
    public GameObject Door2;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Iceblock") || collision.gameObject.CompareTag("Player"))
        {
            Door.SetActive(false);
            Door2.SetActive(false);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Iceblock") || collision.gameObject.CompareTag("Player"))
        {
            Door.SetActive(false);
            Door2.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Iceblock") || collision.gameObject.CompareTag("Player"))
        {
            Door.SetActive(true); 
            Door2.SetActive(true);
        }
    }
}
