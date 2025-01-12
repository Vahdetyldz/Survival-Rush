using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Karakterin hareket h�z�
    private bool isFacingRight; //Karakterin y�n�
    public int playerHealth = 20; //Karakterin can�
    public float weaponRadius ; // Silahlar�n karakterden uzakl���
    public int maxWeapons = 8; // Maksimum silah say�s�

    public GameObject weaponPrefab; // Silah prefab'�
    private List<GameObject> weapons = new List<GameObject>(); // Silah listesi
    private List<Transform> enemies = new List<Transform>(); // D��manlar�n referans�
    public HealthBar healthbar;

    private void Start()
    {
        isFacingRight = true; // Karakterin ba�lang��ta y�z� ne tarafa d�n�k oldu�unu belirler
        SpawnWeapons();
        healthbar.SetMaxHealth(playerHealth);
    }

    void Update()
    {
        UpdateWeaponDirections();

        HandleMovement();
        healthbar.SetHealth(playerHealth);
    }

    private void HandleMovement()
    {
        #region Karakter Hareketi

        // Kullan�c� giri�lerini al
        float moveHorizontal = Input.GetAxis("Horizontal"); // Sa� ve sol ok tu�lar� veya A/D
        float moveVertical = Input.GetAxis("Vertical");     // Yukar� ve a�a�� ok tu�lar� veya W/S

        // Hareket vekt�r� olu�tur
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        // �apraz hareket h�z�n� normal h�za d���r
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        // Karakteri hareket ettir
        transform.Translate(movement * speed * Time.deltaTime);

        #endregion

        #region Karakter Y�n�
        if (moveHorizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && isFacingRight)
        {
            Flip();
        }
        #endregion
    }

    void UpdateWeaponDirections()
    {
        foreach (GameObject weapon in weapons)
        {
            Transform nearestEnemy = FindNearestEnemyForWeapon(weapon.transform);

            if (nearestEnemy != null)
            {
                Vector3 direction = (nearestEnemy.position - weapon.transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                // Debug �izgisi
                Debug.DrawLine(weapon.transform.position, nearestEnemy.position, Color.red);
            }
        }
    }

    Transform FindNearestEnemyForWeapon(Transform weapon)
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (EnemyController enemy in FindObjectsOfType<EnemyController>())
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(weapon.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }
        }

        return nearestEnemy;
    }

    void Flip()
    {
        // Karakterin y�n�n� ters �evir
        isFacingRight = !isFacingRight;

        // Sadece karakterin �l�e�ini ters �evir
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // X eksenini ters �evir
        /*
        transform.localScale = newScale;
        float angleOffset = isFacingRight ? 0 : Mathf.PI;
        for (int i = 0; i < weapons.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / maxWeapons + angleOffset;
            Vector3 newPosition = new Vector3(
                Mathf.Cos(angle) * weaponRadius,
                Mathf.Sin(angle) * weaponRadius,
                0
            );
            weapons[i].transform.localPosition = newPosition;
        }*/
    }


    void SpawnWeapons()
    {
        for (int i = 0; i < maxWeapons/*8*/; i++)
        {
            float angle = i * Mathf.PI * 2 / maxWeapons;
            Vector3 weaponPosition = new Vector3(
                Mathf.Cos(angle) * weaponRadius,
                Mathf.Sin(angle) * weaponRadius,
                0
            );

            GameObject weapon = Instantiate(weaponPrefab, transform.position + weaponPosition, Quaternion.identity);
            weapon.transform.parent = transform; // Silahlar� karaktere ba�la
            weapons.Add(weapon);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                playerHealth -= enemy.damage;

                Destroy(collision.gameObject); // �arp�lan d��man� yok et

                if (playerHealth <= 0)
                {
                    Destroy(gameObject); // Oyuncuyu yok et
                }
            }
        }
    }
}
