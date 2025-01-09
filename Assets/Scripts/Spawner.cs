using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<SpawnableObject> spawnableObjects;  // Nesneler ve olas�l�klar�
    public GameObject gameObjectX;
    private Vector3[] positions;

    public float delay;
    public int maxEnemyNumber = 5;
    public int minEnemyNumber = 1;
    private float temp;

    void Start()
    {
        temp = delay;
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            // SpawnX �a�r�l�yor ve pozisyonlar �retiliyor
            positions = SpawnX();

            // SpawnEnemy fonksiyonu 2 saniye gecikmeli �al��t�r�l�yor
            StartCoroutine(SpawnEnemyWithDelay(positions));

            delay = temp;
        }
    }

    public Vector3[] SpawnX()
    {
        int enemyNumber = Random.Range(minEnemyNumber, maxEnemyNumber);
        Vector3[] positions = new Vector3[enemyNumber];

        for (int i = 0; i < enemyNumber; i++)
        {
            positions[i] = RandomVector();
            Instantiate(gameObjectX, positions[i], Quaternion.identity);
        }

        return positions;
    }

    private System.Collections.IEnumerator SpawnEnemyWithDelay(Vector3[] positions)
    {
        yield return new WaitForSeconds(2f);
        SpawnEnemy(positions);
    }

    public void SpawnEnemy(Vector3[] pos)
    {
        foreach (Vector3 position in pos)
        {
            // Toplam a��rl��� hesapla
            int totalWeight = 0;
            foreach (var obj in spawnableObjects)
            {
                totalWeight += obj.spawnWeight;
            }

            // Rastgele bir say� se�
            int randomWeight = Random.Range(0, totalWeight);

            // Rastgele a��rl��a g�re bir nesne se� ve �ret
            int currentWeight = 0;
            foreach (var obj in spawnableObjects)
            {
                currentWeight += obj.spawnWeight;
                if (randomWeight < currentWeight)
                {
                    Instantiate(obj.prefab, position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    private Vector3 RandomVector()
    {
        float x = Random.Range(-7f, 7f);
        float y = Random.Range(-10f, 7f);
        Vector3 position = new Vector3(x, y, 0);
        return position;
    }
}
