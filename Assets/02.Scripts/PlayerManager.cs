using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Animator animator;
    public GameOver gameOver;
    private int breakBrickCount = 0;


    private IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        GameManager.Instance.LoadData();
        // HPUIManager.Instance�� ������ ������ ���
        while (HPUIManager.Instance == null)
            yield return null;

        HPUIManager.Instance.UpdateHPbar(GameManager.Instance.nowPlayer.curruntHP, GameManager.Instance.nowPlayer.maxHP);
    }

    public void TakeDamage(float damage)
    {
        GameManager.Instance.nowPlayer.curruntHP = Mathf.Max(GameManager.Instance.nowPlayer.curruntHP - damage, 0);
        HPUIManager.Instance.UpdateHPbar(GameManager.Instance.nowPlayer.curruntHP, GameManager.Instance.nowPlayer.maxHP);
        if (GameManager.Instance.nowPlayer.curruntHP <= 0)
        {
            StartCoroutine(PlayerDie());
        }
    }

    public IEnumerator Heal(float healAmount)
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("doHeal");
        GameManager.Instance.nowPlayer.curruntHP = Mathf.Min(GameManager.Instance.nowPlayer.curruntHP + healAmount, GameManager.Instance.nowPlayer.maxHP);
        HPUIManager.Instance.UpdateHPbar(GameManager.Instance.nowPlayer.curruntHP, GameManager.Instance.nowPlayer.maxHP);

        yield return new WaitForSeconds(2f);
    }

    public IEnumerator PlayerDie()
    {
        Debug.Log("Player Died");
        animator.SetTrigger("doDie");
        yield return new WaitForSeconds(2f);
        gameOver.SetGameOver();
        
    }

    public void setBreakBrickCount(int count)
    {
        breakBrickCount = count;
    }
    public int getBreakBrickCount()
    {
        return breakBrickCount;
    }
    public void initBreakBrickCount()
    {
        breakBrickCount = 0;
    }

}
