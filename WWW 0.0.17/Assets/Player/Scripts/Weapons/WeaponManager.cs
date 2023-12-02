using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;
    
    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;

    [Header("Mana Properties")]
    [SerializeField] float maxMana;
    public float currentMana;

    [SerializeField] Image manaBar;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
        currentMana = maxMana;
    }

    
    void Update()
    {
        if (currentMana < maxMana)
        {
            currentMana += Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
            UpdateManaBar();
        }

        if (ShouldFire()) Fire();
    }

    void UpdateManaBar()
    {
        float fillAmount = currentMana / maxMana;
        manaBar.fillAmount = fillAmount;
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate || currentMana <= 20f) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentMana -= 15f; // Уменьшите уровень маны после выстрела
            return true;
        }
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0))
        {
            currentMana -= 1f; // Уменьшите уровень маны после каждого кадра стрельбы
            return true;
        }
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
