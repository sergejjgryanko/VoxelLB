using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damageAmount = 10; // Урон от оружия

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Получение ссылки на скрипт здоровья игрока (если такой имеется)
            
            // Если скрипт здоровья найден, нанести урон игроку
            
        }
    }
}
