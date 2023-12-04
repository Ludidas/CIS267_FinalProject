using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeshiftWall : MonoBehaviour{

    [SerializeField] private GameObject treasure;

    private int i;

    private void Start() {
        i = 0;
    }

    // Update is called once per frame
    void Update(){
        if (treasure == null && i == 0) {
            // make camera shake
            // play sound effect
            // call it a day

            i++;

            this.GetComponent<BoxCollider2D>().enabled = false;

            // grab all children sprite renderers
            SpriteRenderer[] kids = this.GetComponentsInChildren<SpriteRenderer>();

            // get rid of em
            for (int j = 0; j < kids.Length; j++) {
                kids[j].enabled = false;
            }
        }
    }
}
