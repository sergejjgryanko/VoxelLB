using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public GameObject canvasMenu; // Ссылка на CanvasMenu
    public GameObject player; // Ссылка на объект персонажа

    private bool isPaused = false;
    public bool IsPaused
    {
        get { return isPaused; }
    }

    private CharacterController characterController;
    private MonoBehaviour[] playerScripts;

    void Start()
    {
        if (player != null)
        {
            characterController = player.GetComponent<CharacterController>();
            playerScripts = player.GetComponents<MonoBehaviour>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Подписываемся на событие загрузки сцены
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Отписываемся от события перед уничтожением объекта
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Проверяем, есть ли еще ссылка на canvasMenu перед обращением к ней
        if (canvasMenu != null && !canvasMenu.activeSelf && isPaused)
        {
            ResumeGame();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvasMenu.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // Проверяем, если CanvasMenu скрыт, а игра все еще находится на паузе, снимаем паузу
        if (!canvasMenu.activeSelf && isPaused)
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        canvasMenu.SetActive(true);

        if (characterController != null)
        {
            characterController.enabled = false;
        }

        foreach (MonoBehaviour script in playerScripts)
        {
            if (script != null && script != this)
            {
                script.enabled = false;
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        canvasMenu.SetActive(false);

        if (characterController != null)
        {
            characterController.enabled = true;
        }

        foreach (MonoBehaviour script in playerScripts)
        {
            if (script != null && script != this)
            {
                script.enabled = true;
            }
        }
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}