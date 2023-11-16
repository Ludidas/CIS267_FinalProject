using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour{

    
    private FlashingTriangle triangleScript;

    private Queue<string> sentences;

    private bool currentlyTalking; // used in this script so we don't run functions on every keypresses
    private string currentSentence;

    // Vars used for the dialogue typing animation
    private float curTime;
    private float waitTime;
    private int _iterator;

    [Header("References")]
    // Remember to set these when you create a DialogueManager GameObject in your scene
    [SerializeField] public GameObject triangle;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public TMP_Text TMP_Name;
    [SerializeField] public TMP_Text TMP_Dialogue;

    //This will be used to hotswap between control schemes to allow the user to navigate through the dialogues
    [SerializeField] private GameObject inputManagerGO;
    private InputManager inputManager;

    void Awake() {
        // only for making the tiny triangle flash
        // this has to be in Awake() to make our connections first
        triangleScript = triangle.GetComponent<FlashingTriangle>();
    }

    void Start(){
        sentences = new Queue<string>();
        currentlyTalking = false;

        currentSentence = null;
        waitTime = 0.05f; // Speed in which each letter is typed
        curTime = waitTime; // curTime is equal to wait time at first so the first letter is typed right away
        _iterator = 1; // Set to 1 because we are using it for a substring

        //This creates a link to the InputManager script
        inputManager = inputManagerGO.GetComponent<InputManager>();
    }

    private void Update()
    {
        //// I'm 40% sure JoystickButton0 is the A button
        //if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space) && currentSentence == null)
        //{
        //    if (currentlyTalking)
        //    {
        //        displayNextSentence();
        //    }
        //}

        //Had to destroy most of what's in here, except this part seems necessary so I left it.
        //You'll find most of the code that was in here moved into onClickThrough(). -Nick
        if (currentSentence != null)
        {
            typeDialogue();
        }
    }
    
    //Called by the Player Input object on the InputManager gameobject which is a child of Player.
    //This will either speed up the dialogue or move to the next sentence.
    public void onClickThrough(InputAction.CallbackContext value)
    {
        //Value is what is passed by the Player Input. value.started is true when the button is first pressed, then false when the button is released.
        //There is a secret third pass which is also false, no idea where that comes from though. It just usually shows up.
        if (value.started && currentSentence == null)
        {
            if (currentlyTalking)
            {
                Debug.Log("In currentlyTalking");
                displayNextSentence();
            }
        }
        if (currentSentence != null)
        {
            if (value.started && _iterator > 1)
            {
                Debug.Log("In _iterator > 1");
                waitTime = 0f;
            }
            typeDialogue();
        }
    }


    public void startDialogue(Dialogue d) {
        currentlyTalking = true;
        dialogueBox.SetActive(true);

        //This switches the control map to the Dialogue settings, which prevents the player from moving.
        inputManager.swapMap("Dialogue");

        // clear queue if there is one
        sentences.Clear();

        // build Queue
        for (int i = 0; i < d.sentences.Length; i++) {
            sentences.Enqueue(d.sentences[i]);
        }

        // set the name of who we are talking to
        TMP_Name.text = d.name;
        // display first sentence
        displayNextSentence();
    }

    private void displayNextSentence() {
        // for blinking triangle
        triangleScript.setTyping(true);

        if (sentences.Count > 0) {
            currentSentence = sentences.Dequeue();
            // Displaying the sentence in the dialogue box is done in the Update function
        } else {
            endDialogue();
        }
    }

    private void endDialogue() {
        dialogueBox.SetActive(false);
        currentlyTalking = false;

        //This switches the control map to the PlayerControls settings, which allows the player to move again.
        inputManager.swapMap("PlayerControls");

        // it isn't typing, but we don't want the triangle to blink anymore
        // i don't think it matters because the whole dialogue box is inactive
        triangleScript.setTyping(true);
    }

    private void typeDialogue() {
        const string funnyStar = "* ";

        if (curTime < waitTime) {
            curTime += Time.deltaTime;
        } else {
            curTime = 0f;
            _iterator++;
            TMP_Dialogue.text = funnyStar + currentSentence.Substring(0, _iterator);
        }

        if (_iterator >= currentSentence.Length) {
            triangleScript.setTyping(false);
            resetDialogueVars();
        }
    }

    private void resetDialogueVars() {
        _iterator = 1;
        waitTime = 0.05f;
        curTime = waitTime;
        currentSentence = null;
    }
}
