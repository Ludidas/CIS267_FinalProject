using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour{

    public GameObject triangle;
    private FlashingTriangle triangleScript;

    private Queue<string> sentences;
    
    // Remember to set these when you create a DialogueManager GameObject in your scene
    public GameObject dialogueBox;
    public TMP_Text TMP_Name;
    public TMP_Text TMP_Dialogue;

    private bool currentlyTalking; // used in this script so we don't run functions on every keypresses
    private string currentSentence;

    // Vars used for the dialogue typing animation
    private float curTime;
    private float waitTime;
    private int _iterator;


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
    }

    private void Update() {
        // I'm 40% sure JoystickButton0 is the A button
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space) && currentSentence == null) {
            if (currentlyTalking) {
                displayNextSentence();
            }
        }

        if (currentSentence != null) {
            // _iterator > 1 so we don't enter on the same key down that displays the next sentence
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space) && _iterator > 1) {
                // waitTime is reset when typeDialogue() ends
                waitTime = 0f;
            }
            typeDialogue();
        }
    }


    public void startDialogue(Dialogue d) {
        currentlyTalking = true;
        dialogueBox.SetActive(true);

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
