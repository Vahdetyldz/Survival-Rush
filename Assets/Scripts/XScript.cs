using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class XScript : MonoBehaviour
{
    async void Start()
    {
        await Task.Delay(2000);
        if (Application.isPlaying) // oyunun �al���p �al��mad���n� kontrol ediyor
        {
            Destroy(gameObject);
        }

    }
}
