using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FlashingTriangle : MonoBehaviour{

    private float cycleTime;
    private bool typing;
    private float curTime;
    private bool active;

    private Image img;

    void Awake() {
        img = gameObject.GetComponent<Image>();
    }

    void Start(){
        active = false;
        curTime = 0f;
        cycleTime = .8f;
        typing = true;
    }

    // Update is called once per frame
    void Update(){
        if (!typing) {
            flashTriangle();
        }
    }

    private void flashTriangle() {
        if (curTime < cycleTime) {
            curTime += Time.deltaTime;
        } else {
            active = !active;
            curTime = 0f;
            img.enabled = active;
        }
    }

    public void setTyping(bool isTyping) {
        typing =  isTyping;
        curTime = 0f;

        //we can set active to true because we always want to start cycling from active
        active = true;

        // if we are typing gameObject will be inactive
        // if we aren't typing gameObject will be active
        img.enabled = !typing;
    }
}
