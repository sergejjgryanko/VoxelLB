using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton; // Ссылка на кнопку "Start Game"

    void Start()
    {
        // Назначаем функцию StartGame на нажатие кнопки
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        // Здесь можно добавить логику для начала игры
        // Например, загрузить первый уровень или выполнить действия перед началом игры
        SceneManager.LoadScene("Level1"); // Загрузить сцену игры
    }
}
