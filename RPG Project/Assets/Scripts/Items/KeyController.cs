using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour{

    [SerializeField] private GameObject key1;
    [SerializeField] private GameObject key2;
    [SerializeField] private GameObject door;

    [SerializeField] private GameObject dManagerGO;
    private DialogueManager dManager;

    private Dialogue firstMessage = new Dialogue();

    private Dialogue[] conversation = new Dialogue[2];
    private Dialogue secondMessage = new Dialogue();
    private Dialogue thirdMessage = new Dialogue();


    private int iterator;

    // Start is called before the first frame update
    void Start(){
        if (dManagerGO != null) {
            dManager = dManagerGO.GetComponent<DialogueManager>();
        } else {
            // yolo
            Debug.Log("KeyController script found dManager by 'find' command");
            dManager = FindObjectOfType<DialogueManager>();
        }
        iterator = 0;

        firstMessage.name = "Dusty";

        secondMessage.name = "Dusty";
        thirdMessage.name = "";


        firstMessage.sentences = new string[] {
            "Half of a key really doesn't do me any good.",
            "I should look for the other half of it." 
        };

        // not used rn
        secondMessage.sentences = new string[] {
            "Another part of a key!"
        };

        thirdMessage.sentences = new string[] {
            "The key vanishes from your hand as you go to put them together",
            "You get an unsettling feeling that the path forward is no longer blocked"
        };

        conversation[0] = secondMessage; conversation[1] = thirdMessage;

        // Set first dialogue
    }

    // Update is called once per frame
    void Update(){
        if (key1 == null && key2 == null && iterator == 1) {
            iterator = -999;

            dManager.startConversation(conversation);

            door.SetActive(false);

        }else if (key1 == null && iterator == 0) {
            iterator++;
            dManager.startDialogue(firstMessage);

        } else if (key2 == null && iterator == 0) {
            iterator++;
            dManager.startDialogue(firstMessage);
        }
    }
}
