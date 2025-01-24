using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject x;
    public GameObject purchaseMenu;
    public Text timerText;

    public int currentWave = 0; // �u anki dalga
    public int maxWaves = 10; // Maksimum dalga say�s�
    private float remainingTime = 20f; 
    private bool isTimerRunning = true;
    float temp = 0;

    private void Start()
    {
        purchaseMenu.SetActive(false);
        temp = remainingTime;
        StartCoroutine(StartWave());
    }
    void Update()
    {
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                isTimerRunning = false;
                remainingTime = 0;
                UpdateTimerDisplay();
                OnTimerEnd();
            }
        }

    }
    IEnumerator StartWave()
    {
        while (currentWave < maxWaves)
        {
            currentWave++;

            Debug.Log("Dalga Ba�lad�: " + currentWave);

            yield return new WaitForSeconds(remainingTime); // Dalga s�resi 

            Debug.Log("Dalga Bitti: " + currentWave);

            // Dalga bitince men�y� a�
            OpenPurchaseMenu();

            // Men� kapal� kal�rken bekle
            yield return new WaitUntil(() => !purchaseMenu.activeSelf);
        }
    }
    public void OpenPurchaseMenu()
    {
        purchaseMenu.SetActive(true);
        Time.timeScale = 0; // Oyunu durdur
    }
    public void ClosePurchaseMenu()
    {
        purchaseMenu.SetActive(false);
        Time.timeScale = 1; // Oyunu devam ettir
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        
        /*
         -Ekrandaki d��manlar� yok et 
         -Paralar� toplat
        -Can� yenile
        */
        remainingTime += temp + 5f;
        temp = remainingTime;
        isTimerRunning = true;
    }
}
