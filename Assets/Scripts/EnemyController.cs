using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 5; // Düþmanýn caný
    public int damage=1; //Oyuncuya vereceði hasar
    public float speed = 2f; // Düþmanýn hareket hýzý
    private Transform player; // Oyuncunun Transform'u

    void Start()
    {
        // Oyuncuyu bul (tag'i "Player" olarak ayarlamalýsýnýz)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // Eðer oyuncu sahnede varsa, düþmaný oyuncuya doðru hareket ettir
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized; // Oyuncuya olan yön
            transform.position += direction * speed * Time.deltaTime; // Düþmaný hareket ettir
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Düþmaný yok et
        Destroy(gameObject);
    }
}
