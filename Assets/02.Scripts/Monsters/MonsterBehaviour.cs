using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{

    public MonsterStat stat;
    public float curruntHP;
    public int posIndex;

    public void Init(MonsterStat stat)
    {
        this.stat = stat;
        curruntHP = stat.hp;
    }

    public float MonsterAttack()
    {
        float damage = stat.attack;

        return damage;
    }

    public void TakeDamage(float damage)
    {
        curruntHP -= damage;
        Debug.Log($"Monster took {damage} damage, current HP: {curruntHP}");
 
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
        Destroy(this.gameObject);

        // 몬스터가 죽을 때 Destory하여 코루틴이 동작을 안하기 때문에 따로 NextTurn을 호출
        GameManager.Instance.NextTurn();


    }

    public float GetCurrentHP()
    {
        return curruntHP;
    }
}
