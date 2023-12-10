using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье игрока
    private int currentHealth; // Текущее здоровье игрока
    public Image healthBar; // Ссылка на HealthBar (Image)
    public GameObject canvasDeath; // Ссылка на CanvasDeath

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHealth = maxHealth; // Установка начального здоровья
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшение здоровья при получении урона
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ограничение здоровья от 0 до maxHealth
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Активируем CanvasDeath при смерти игрока
        canvasDeath.SetActive(true);
    }

    void UpdateHealthBar()
    {
        float healthPercent = (float)currentHealth / maxHealth; // Рассчет процента здоровья
        healthBar.fillAmount = healthPercent; // Обновление заполнения HealthBar в соответствии с процентом здоровья
    }
}
