using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject x;

    public Text timerText;
    public float remainingTime = 300f;
    private bool isTimerRunning = true;
    void Update()
    {
        if (isTimerRunning)
        {
            if (remainingTime>0)
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
    private Vector3 RandomVector()
    {
        float x = Random.Range(-7f,7f);
        float y = Random.Range(-10f,7f);
        Vector3 position = new Vector3(x,y,0);
        return position;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        // Sayaç bittiðinde yapýlacak iþlemler...
    }
}
