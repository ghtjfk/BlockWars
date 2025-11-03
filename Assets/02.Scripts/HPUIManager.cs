using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{

    public static HPUIManager Instance;
    public Image HealthPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //코루틴으로 GameManager 인스턴스가 생성될 때까지 대기

    private void Start()
    {
        UpdateHPbar(80f, 100f);

    }
    public void UpdateHPbar (float curruntHP, float maxHP)
    {
        HealthPoint.fillAmount = curruntHP / maxHP;
    }

    public void RefreshHPBarGameManager()
    {
        float curruntHP = GameManager.Instance.currentHP;
        float maxHP = GameManager.Instance.maxHP;
        UpdateHPbar(curruntHP, maxHP);
    }
}
