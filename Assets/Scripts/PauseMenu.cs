using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    public Button MenuButton; // references to all of the buttons on the pause menu
    public Button ResumeButton;
    public Button OptionsButton;
    public Button QuitButton;
    private bool isPaused = false;  

    void Start()
    {   //assign functions to each of my buttons!!! (finally :) )
        MenuButton.onClick.AddListener(TogglePauseMenu);
        ResumeButton.onClick.AddListener(ResumeGame);
        QuitButton.onClick.AddListener(QuitGame);
        pauseMenuUI.SetActive(false);  // this makes sure the menu is hidden on startup
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    
    void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // show the pause menu
        Time.timeScale = 0f;          // freeze game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // hide pause menu
        Time.timeScale = 1f;          // resume game
        isPaused = false;
        //had to add this because every time I clicked out of the pause menu and pressed space, the game would repause
         
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitGame()
    {
        //Debug.Log("Quitting Game...");
        Application.Quit();
    }
}