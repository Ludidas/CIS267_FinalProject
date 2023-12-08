using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour{

    [SerializeField] private Sprite[] hearts;

    private float localHealth;

    void Start(){
        updateUI();
    }

    void Update(){
        if (localHealth != GameManager.getHealth()) {
            updateUI();
        }
    }

    private void updateUI() {
        localHealth = GameManager.getHealth();

        int i = selectCorrectImage();

        GetComponent<Image>().sprite = hearts[i];
    }

    private int selectCorrectImage() {
        // I woild have used a switch statement or something but health is a float

        // legnth 12 
        for (int i = 0; i < hearts.Length; i++) {
            if (i >= localHealth) {
                return i;
            }
        }

        return 0;
    }
    
}
