using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===============================
// Pretty much the same thing as LoadZone but I didn't want to load the scene again
//===============================

public class Teleport : MonoBehaviour{

    [SerializeField] private float xDestination;
    [SerializeField] private float yDestination;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {
            collision.transform.parent.gameObject.transform.position = new Vector3 (xDestination, yDestination, 0);
        }
    }
}
