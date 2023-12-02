using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] int damage = 25; // Добавляем переменную для уронам
    float timer;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy) Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Передаем урон объекту врага
            }
        }

        Destroy(this.gameObject);
    }
}
