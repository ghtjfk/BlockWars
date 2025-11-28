using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{

    public MonsterStat stat;
    public float curruntHP;
    public int posIndex;
    SpriteRenderer spriteRenderer; // 몬스터의 SpriteRenderer 컴포넌트
    Color originalColor;           // 몬스터의 원래 색깔 저장
    bool isHitEffectActive = false; // 피격 효과 중인지 확인하는 플래그

    public void Init(MonsterStat stat)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
       
        this.stat = stat;
        curruntHP = stat.hp;
    }

    public float MonsterAttack()
    {
        PlayerManager.Instance.animator.SetTrigger("doHit");
        float damage = stat.attack;

        return damage;
    }

    public void TakeDamage(float damage)
    {
        if(isHitEffectActive)
        {
            return;
        }


        curruntHP -= damage;
        Debug.Log($"Monster took {damage} damage, current HP: {curruntHP}");

        StartCoroutine(HitEffectCoroutine(1f));



    }

    public void MonsterHeal()
    {
        float damage = stat.attack;

        curruntHP += damage;

    }

    public void MonsterDie()
    {
        //GameManager.Instance.addCoin(stat.coin);
        if (MonsterManager.Instance != null)
        {
            MonsterManager.Instance.RemoveMonster(this);
        }

        TurnManager.Instance.startDeadMonsterSequence(0f, this.gameObject);


    }

    public float GetCurrentHP()
    {
        return curruntHP;
    }

    IEnumerator HitEffectCoroutine(float duration)
    {
        isHitEffectActive = true; // 피격 효과 시작 플래그 설정

        // 몬스터 스프라이트의 색깔을 빨간색으로 변경
        spriteRenderer.color = Color.red;

        // 지정된 시간(duration) 동안 기다림
        yield return new WaitForSeconds(duration);

        // 기다린 후 몬스터 스프라이트의 색깔을 원래 색깔로 되돌림
        spriteRenderer.color = originalColor;

        isHitEffectActive = false; // 피격 효과 종료 플래그 해제
    }
}


