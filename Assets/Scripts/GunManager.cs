using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public float fireRate = 1f;       // Ate� etme h�z� (saniye)
    public int damage = 10;          // Silah�n hasar g�c�
    public float range = 10f;        // Maksimum menzil
    public GameObject bulletPrefab;  // Mermi prefab'�
    public Transform firePoint;      // Merminin ��k�� noktas�

    private float fireCooldown = 0f; // Ate� zamanlay�c�s�
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
            fireCooldown = fireRate; // Ate� h�z�n� s�f�rla
        }
    }

    void FireAtClosestEnemy()
    {
        EnemyController closestEnemy = FindClosestEnemy();

        if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) <= range)
        {
            // Mermiyi olu�tur ve y�nlendir
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<BulletManager>().damage = damage;

            Vector3 direction = (closestEnemy.transform.position - firePoint.position).normalized;
            bullet.transform.right = direction; // Mermiyi hedefe y�nlendir

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
