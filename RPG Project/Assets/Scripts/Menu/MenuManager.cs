using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour{
    [SerializeField] private GameObject inputManagerGO;
    [SerializeField] private GameObject pauseMenu;

    private InputManager inputManager;

    public void Awake() {
        inputManager = inputManagerGO.GetComponent<InputManager>();
    }

    public void resumeGame() {
        // Correct timescale
        Time.timeScale = 1.0f;

        // Disable Pause Menu
        pauseMenu.SetActive(false);

        // Swap Player Controls
        inputManager.swapMap("PlayerControls");
    }

    public void pauseGame() {
        // Pause game
        Time.timeScale = 0f;

        // Enable Pause menu
        pauseMenu.SetActive(true);

        // Swap Player Controls
        inputManager.swapMap("Menu");
    }

    public void showControls() {
        // I still need to make this part
    }

    public void loadTitleScreen() {
        // Currently fails because of our load order
        SceneManager.LoadScene("TitleScreen");
    }

    public void exitGame() {
        Application.Quit();
    }
}
