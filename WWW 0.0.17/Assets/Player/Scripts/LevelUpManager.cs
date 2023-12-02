using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public MovementStateManager movementStateManager;

    public Text levelText;

    void Start()
    {
        // Получите компонент Text из текущего объекта
        levelText = GetComponentInChildren<Text>();

    }

    void Update()
    {
        // Обновите текст с текущим уровнем из MovementStateManager
        levelText.text = "LEVEL: " + movementStateManager.level.ToString();

        // Пример: если достигнуты условия для повышения уровня (можно изменить)
        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        // Увеличение уровня
        movementStateManager.IncreaseLevel();
    }
}
