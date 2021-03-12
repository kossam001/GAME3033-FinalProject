using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject pauseMenu;

    private bool isPaused = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;
        pauseMenu = GameObject.Find("PauseMenu");
        
        if (pauseMenu == null) return;
        
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if (pauseMenu == null) return;

        if (isPaused)
        {
            Time.timeScale = 1.0f;

            isPaused = false;
            pauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0.0f;

            isPaused = true;
            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Quit()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Application.Quit();
    }
}
