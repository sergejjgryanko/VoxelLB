using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasDeath : MonoBehaviour
{
    public void RestartLevel()
    {
        // Перезагрузка текущей сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        // Загрузка главного меню
        SceneManager.LoadScene("MainMenu");
    }
}
