using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{

    public static HPUIManager instance;

    public Image HealthPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void UpdateHPbar (float curruntHP, float maxHP)
    {
        HealthPoint.fillAmount = curruntHP / maxHP;
    }
}
