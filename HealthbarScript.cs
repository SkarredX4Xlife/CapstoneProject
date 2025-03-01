using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script : MonoBehaviour
{
    public Image Healthbar_Fill;

    public void UpdateHealthbar(float healthAmount)
    {
        Healthbar_Fill.fillAmount = healthAmount;
    }
}
