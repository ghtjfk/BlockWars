using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HPUIManager : Singleton<HPUIManager>, IPointerEnterHandler, IPointerExitHandler
{

    public Image HealthPoint;
    public Text HPText;

    public void UpdateHPbar(float curruntHP, float maxHP)
    {
        float percent = curruntHP / maxHP;
        Debug.Log($"fillAmount: {percent}");
        HealthPoint.fillAmount = percent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HPText.text = GetHPText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HPText.text = "";
    }
    string GetHPText()
    {
        float curruntHP = GameManager.Instance.nowPlayer.curruntHP;
        float maxHP = GameManager.Instance.nowPlayer.maxHP;
        return $"{curruntHP} / {maxHP}";
    }
}
