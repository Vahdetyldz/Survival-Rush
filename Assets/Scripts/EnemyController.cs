using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 5; // D��man�n can�
    public int damage=1; //Oyuncuya verece�i hasar
    public float speed = 2f; // D��man�n hareket h�z�
    private Transform player; // Oyuncunun Transform'u

    void Start()
    {
        // Oyuncuyu bul (tag'i "Player" olarak ayarlamal�s�n�z)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // E�er oyuncu sahnede varsa, d��man� oyuncuya do�ru hareket ettir
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized; // Oyuncuya olan y�n
            transform.position += direction * speed * Time.deltaTime; // D��man� hareket ettir
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
        // D��man� yok et
        Destroy(gameObject);
    }
}
