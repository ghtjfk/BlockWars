using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : Singleton<HPUIManager>
{

    public Image HealthPoint;


    public void UpdateHPbar(float curruntHP, float maxHP)
    {
        float percent = curruntHP / maxHP;
        Debug.Log($"fillAmount: {percent}");
        HealthPoint.fillAmount = percent;
    }
}
