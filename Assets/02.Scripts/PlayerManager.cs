using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    float maxHP = 100f;
    float curruntHP;
    float attackDamage = 5f;

    public Animator animator;


    private IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        curruntHP = maxHP;
        // HPUIManager.Instance가 생성될 때까지 대기
        while (HPUIManager.Instance == null)
            yield return null;

        HPUIManager.Instance.UpdateHPbar(curruntHP, maxHP);
    }

    public float getAttackDamage()
    {
        return attackDamage;
    }
    public void TakeDamage(float damage)
    {
        curruntHP = Mathf.Max(curruntHP - damage, 0);
        HPUIManager.Instance.UpdateHPbar(curruntHP, maxHP);
        if (curruntHP <= 0)
        {
            PlayerDie();
        }
    }

    public IEnumerator Heal(float healAmount)
    {
        animator.SetTrigger("doHeal");
        curruntHP = Mathf.Min(curruntHP + healAmount, maxHP);
        HPUIManager.Instance.UpdateHPbar(curruntHP, maxHP);

        yield return new WaitForSeconds(2f);
    }

    public void PlayerDie()
    {
        Debug.Log("Player Died");
        animator.SetTrigger("doDie");
        //GameManager.Instance.GameOver();
    }
    public float GetCurrentHP()
    {
        return curruntHP;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }
    public void init()
    {
        maxHP = 100f;
        attackDamage = 5f;
    }

}
