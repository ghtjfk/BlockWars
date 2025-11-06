using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{

    float maxHP = 100f;
    float curruntHP;
    float attackDamage = 5f;
    public bool isPlayerTurn = true;

    private IEnumerator Start()
    {
        curruntHP = maxHP;
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

    public void init()
    {
        maxHP = 100f;
        attackDamage = 5f;
    }

}
