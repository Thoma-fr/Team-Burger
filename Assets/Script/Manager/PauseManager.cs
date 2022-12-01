using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance { get; private set; }

    private static bool isGamePaused = false;
    private CanvasGroup pauseMenu;
    private PlayerController.PLAYER_MODE previousMode;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        pauseMenu = gameObject.GetComponent<CanvasGroup>();

        pauseMenu.alpha = 0;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            previousMode = PlayerController.Instance.playerMode;
            PlayerController.Instance.playerMode = PlayerController.PLAYER_MODE.PAUSED;
            Time.timeScale = 0;
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
        }
        else
        {
            PlayerController.Instance.playerMode = previousMode;
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
