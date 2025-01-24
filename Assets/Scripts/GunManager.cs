using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public float fireRate = 1f;       // Ateþ etme hýzý (saniye)
    public int damage = 10;          // Silahýn hasar gücü
    public float range = 10f;        // Maksimum menzil
    public GameObject bulletPrefab;  // Mermi prefab'ý
    public Transform firePoint;      // Merminin çýkýþ noktasý

    private float fireCooldown = 0f; // Ateþ zamanlayýcýsý
    public AudioSource aud;
    void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
        else
        {
            FireAtClosestEnemy();
            fireCooldown = fireRate; // Ateþ hýzýný sýfýrla
        }
    }

    void FireAtClosestEnemy()
    {
        EnemyController closestEnemy = FindClosestEnemy();

        if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) <= range)
        {
            // Mermiyi oluþtur ve yönlendir
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<BulletManager>().damage = damage;

            Vector3 direction = (closestEnemy.transform.position - firePoint.position).normalized;
            bullet.transform.right = direction; // Mermiyi hedefe yönlendir

            aud.Play();
        }
    }

    EnemyController FindClosestEnemy()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        EnemyController closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyController enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
