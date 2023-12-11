using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void Start()
    {
        if(GameManager.getTotalDungeonsCompleted() == 2)
            {
                Destroy(this.gameObject);
            }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.getTotalDungeonsCompleted() == 2)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
