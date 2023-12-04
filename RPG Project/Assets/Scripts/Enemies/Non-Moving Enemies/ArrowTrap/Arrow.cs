using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour{

    private bool moving;
    public bool Moving {
        get { return moving; }
        set { 
            moving = value;
            if (moving) {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void Awake() {
        //moving is set by ArrowTrap
        //but it should be false
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // deal damage to the player

            Destroy(this.gameObject);
        }
    }
}
