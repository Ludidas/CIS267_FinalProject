using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour{

    // Dialogue is a class that contains 
    // string name
    // string[] sentences

    // You add the attributes when you attach this script to your game object.


    public Dialogue[] conversation;

    public Dialogue[] triggerConversation() {
        return conversation;
    }
}
