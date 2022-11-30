using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private static bool isGamePaused = false;
    private CanvasGroup pauseMenu;

    private void Start()
    {
        pauseMenu = gameObject.GetComponent<CanvasGroup>();

        pauseMenu.alpha = 0;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0;
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.alpha = 0;
            pauseMenu.interactable = false;
            pauseMenu.blocksRaycasts = false;
        }
    }

    public void Resume()
    {
        PauseGame();
    }

    public void MainScene()
    {
        Time.timeScale = 1;
        LevelLoader.instance.LoadMenuScene();
    }

    public void Quit()
    {
        Time.timeScale = 1;
        LevelLoader.instance.ExitApplication();
    }
}
