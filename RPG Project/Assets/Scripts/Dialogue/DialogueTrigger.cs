using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour{

    // Dialogue is a class that contains 
    // string name
    // string[] sentences

    // You add the attributes when you attach this script to your game object.


    public Dialogue dialogue;

    public void triggerDialogue() {
        // I know its bad to use findObjectOfType
        // BUT we are only doing this when a dialogue is triggered and I don't want to have to drag the object into every single thing that can have dialogue
        // please forgive me
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }
}
