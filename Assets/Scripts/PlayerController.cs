using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Karakterin hareket hýzý
    private bool isFacingRight; //Karakterin yönü
    public int playerHealth = 20; //Karakterin caný
    public float weaponRadius ; // Silahlarýn karakterden uzaklýðý
    public int maxWeapons = 8; // Maksimum silah sayýsý

    public GameObject weaponPrefab; // Silah prefab'ý
    private List<GameObject> weapons = new List<GameObject>(); // Silah listesi
    private List<Transform> enemies = new List<Transform>(); // Düþmanlarýn referansý
    public HealthBar healthbar;

    private void Start()
    {
        isFacingRight = true; // Karakterin baþlangýçta yüzü ne tarafa dönük olduðunu belirler
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

        // Kullanýcý giriþlerini al
        float moveHorizontal = Input.GetAxis("Horizontal"); // Sað ve sol ok tuþlarý veya A/D
        float moveVertical = Input.GetAxis("Vertical");     // Yukarý ve aþaðý ok tuþlarý veya W/S

        // Hareket vektörü oluþtur
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        // Çapraz hareket hýzýný normal hýza düþür
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        // Karakteri hareket ettir
        transform.Translate(movement * speed * Time.deltaTime);

        #endregion

        #region Karakter Yönü
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

                // Debug çizgisi
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
        // Karakterin yönünü ters çevir
        isFacingRight = !isFacingRight;

        // Sadece karakterin ölçeðini ters çevir
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // X eksenini ters çevir
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
            weapon.transform.parent = transform; // Silahlarý karaktere baðla
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

                Destroy(collision.gameObject); // Çarpýlan düþmaný yok et

                if (playerHealth <= 0)
                {
                    Destroy(gameObject); // Oyuncuyu yok et
                }
            }
        }
    }
}
