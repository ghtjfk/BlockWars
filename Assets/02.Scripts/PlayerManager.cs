using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{

    float maxHP = 100;
    float curruntHP;


    private IEnumerator Start()
    {
        curruntHP = 50f;
        // HPUIManager.Instance가 생성될 때까지 대기
        while (HPUIManager.Instance == null)
            yield return null;

        HPUIManager.Instance.UpdateHPbar(curruntHP, maxHP);
    }

    public void TakeDamage(float damage)
    {
        curruntHP = Mathf.Max(curruntHP - damage, 0);
        HPUIManager.Instance.UpdateHPbar(curruntHP, maxHP);
    }

    public void Heal(float healAmount)
    {
        curruntHP = Mathf.Min(curruntHP + healAmount, maxHP);
        HPUIManager.Instance.UpdateHPbar(curruntHP, maxHP);
    }
    public float GetCurrentHP()
    {
        return curruntHP;
    }
}
