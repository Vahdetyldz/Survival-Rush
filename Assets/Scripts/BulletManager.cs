using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 10;
    private float extinctionPeriod = 1.5f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        extinctionPeriod -= Time.deltaTime;
        if (extinctionPeriod < 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
