using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject inputManagerGO;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject controlsDisplay;
    [SerializeField] private GameObject eventSystem;

    [SerializeField] private GameObject gameOverDisplay;

    [Header("MenuButtons")]
    [SerializeField] private GameObject btnFirstMenu;
    [SerializeField] private GameObject btnControlsMenu;
    [SerializeField] private GameObject btnCreditsMenu;
    [SerializeField] private GameObject btnGameOver;

    private void Awake() {
        
    }

    public void resumeGame() {
        // Correct timescale
        Time.timeScale = 1.0f;

        // Disable Pause Menu
        canvas.SetActive(false);

        // Swap Controls
        inputManagerGO.GetComponent<InputManager>().swapMap("PlayerControls");
    }

    public void pauseGame() {
        // Pause game
        Time.timeScale = 0f;

        // Enable Pause menu
        canvas.SetActive(true); 

        // Swap Player Controls
        inputManagerGO.GetComponent<InputManager>().swapMap("Menu");
    }

    public void showControls() {

        pauseMenu.SetActive(false);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(btnControlsMenu);

        controlsDisplay.SetActive(true);
    }

    public void hideControls() {
        pauseMenu.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(btnFirstMenu);

        controlsDisplay.SetActive(false);
    }

    public void loadTitleScreen() {
        //VERY IMPORTANT FOR IF PLAYER TRIES TO START A NEW GAME
        Time.timeScale = 1.0f;
        inputManagerGO.GetComponent<InputManager>().swapMap("PlayerControls");

        SceneManager.LoadScene("TitleScreen");
    }

    public void startNewGame() {
        InventorySystem.resetInventory();
        GameManager.newGame();

        SceneManager.LoadScene("HubWorld");
    }

    public void showCredits() {
        pauseMenu.SetActive(false);
       
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(btnCreditsMenu);

        creditsMenu.SetActive(true);
    }
    public void hideCredits() {
        pauseMenu.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(btnFirstMenu);

        creditsMenu.SetActive(false);
    }

    public void exitGame() {
        Application.Quit();
    }

    public void showFinalMenu() {
        pauseGame();

        canvas.SetActive(true);

        gameOverDisplay.SetActive(true);

        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(btnGameOver);

        pauseMenu.SetActive(false);

        gameOverDisplay.SetActive(true);
    }
}
