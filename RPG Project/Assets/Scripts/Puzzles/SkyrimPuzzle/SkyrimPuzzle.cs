using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SkyrimPuzzle : MonoBehaviour{

    [SerializeField] public int state;
    [SerializeField] private Sprite[] sprites;

    private int arraySize;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (state < 0 || state >= sprites.Length) {
            state = 0;
        }
    }


    void Start(){
        spriteRenderer.sprite = sprites[state];
    }

    public void advanceState() {
        if (state == sprites.Length-1) {
            state = 0;
        } else {
            state++;
        }

        updateCurSprite();

        GetComponentInParent<SkyrimChecker>().checkSolution();
    }

    private void updateCurSprite() {
        spriteRenderer.sprite = sprites[state];
    }
}
