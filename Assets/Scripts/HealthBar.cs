using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text textHealth;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        textHealth.text = string.Format("{0:0}/{1:0}",slider.value.ToString(), slider.maxValue.ToString());
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        textHealth.text = string.Format("{0:0}/{1:0}", slider.value.ToString(), slider.maxValue.ToString());
    }
}
