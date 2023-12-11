using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeshiftWall : MonoBehaviour{

    [SerializeField] private GameObject treasure;
    [SerializeField] private GameObject dialogueManagerGO;

    [SerializeField] private float smoothness;
    [SerializeField] private float speed;
    [SerializeField] private float xDisplacement;
    [SerializeField] private float yDisplacement;

    private int doOnce;

    private void Start() {
        doOnce = 0;
    }

    // Update is called once per frame
    void Update(){
        // yeah, we be checking random shit every frame
        if (treasure == null && doOnce == 0) {
            // to add
            // make camera shake
            // play sound effect

            doOnce++;

            GetComponent<BoxCollider2D>().enabled = false;

            // grab all children sprite renderers
            SpriteRenderer[] kids = GetComponentsInChildren<SpriteRenderer>();

            // get rid of em
            for (int j = 0; j < kids.Length; j++) {
                kids[j].enabled = false;
            }

            GameManager.setDungeonCompletion(true, Dungeon.Catacomb);

            Dialogue d = new Dialogue();
            d.name = "Dusty";
            d.sentences = new string[] { "Pog!" };

            
            dialogueManagerGO.GetComponent<DialogueManager>().startDialogue(d);
        }

        if (doOnce == 1) {
            shakeScreen();
        }
    }

    private void shakeScreen() {

    }


}
