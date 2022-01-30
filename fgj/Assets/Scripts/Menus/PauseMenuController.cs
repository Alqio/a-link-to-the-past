using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool isGamePaused()
    {
        return GameIsPaused;
    }

    public GameObject PauseMenu;
    public Canvas PauseCanvas;

    void Awake()
    {
        GameIsPaused = false;
        PauseMenu.SetActive(GameIsPaused);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Debug.Log("Pausing");
            Pause();
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        PauseCanvas.enabled = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        PauseCanvas.enabled = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Paused");
    }

    public void LoadMenu()
    {
        Debug.Log("menuing");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("hävisit pelin");
        Application.Quit();
    }
}

