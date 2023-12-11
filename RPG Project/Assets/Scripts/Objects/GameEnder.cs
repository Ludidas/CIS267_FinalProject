using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour {

    [SerializeField] private GameObject collisionBox;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject inputManagerGO;
    [SerializeField] private GameObject eventManagerGO;
    [SerializeField] private GameObject goBackToMainMenu;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject menuManager;

    private bool checkForInteract;

    private void Awake() {

        if (GameManager.getTotalDungeonsCompleted() >= 3) {
            collisionBox.SetActive(true);
            checkForInteract = true;
        } else {
            checkForInteract = false;
        }
    }

    private void Update() {
        if (checkForInteract) {
            if (collisionBox == null) { // we interacted with the podium
                menuManager.GetComponent<MenuManager>().showFinalMenu();


                checkForInteract = false;

            }
        }
    }
}
