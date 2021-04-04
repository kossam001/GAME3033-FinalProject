using System;
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

    public Mission missionData;

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
        SetPauseMenu();
    }

    private void SetPauseMenu()
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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadGame()
    {
        string loadedData = PlayerPrefs.GetString("PlayerData", "");

        if (loadedData == "") return;

        string[] parsedData = loadedData.Split(',');

        InventoryController.Instance.money = Int32.Parse(parsedData[0]);
        InventoryController.Instance.statSheet.health = Int32.Parse(parsedData[1]);
        InventoryController.Instance.statSheet.attack = Int32.Parse(parsedData[2]);
        InventoryController.Instance.statSheet.defense = Int32.Parse(parsedData[3]);

        InventoryController.Instance.inventory.OnLoad();
    }

    public void SaveGame()
    {
        // Format: money,health,attack,defense
        string money = InventoryController.Instance.money.ToString();
        string health = InventoryController.Instance.statSheet.health.ToString();
        string attack = InventoryController.Instance.statSheet.attack.ToString();
        string defense = InventoryController.Instance.statSheet.defense.ToString();

        PlayerPrefs.SetString("PlayerData", string.Concat(money, ",", health, ",", attack, ",", defense));
        PlayerPrefs.Save();

        InventoryController.Instance.inventory.OnSave();
    }

    public void CreateGame()
    {
        InventoryController.Instance.money = 1000;
        InventoryController.Instance.statSheet.health = 100;
        InventoryController.Instance.statSheet.attack = 0;
        InventoryController.Instance.statSheet.defense = 0;
    }
}
