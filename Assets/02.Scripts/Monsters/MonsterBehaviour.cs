using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{

    MonsterStat stat;
    float curruntHP;

    public void init(MonsterStat stat)
    {
        this.stat = stat;
        curruntHP = stat.hp;
    }

    public float monsterAttack()
    {
        int stage = GameManager.Instance.getStage();
        float damage = (1.0f + 0.1f * stage) * stat.attack + Random.Range(0, stage);

        return damage;
    }

    public void takeDamage(float damage)
    {
        curruntHP -= damage;
        if (curruntHP <= 0)
        {
            monsterDie();
        }
 
    }

    public void monsterHeal()
    {
        int stage = GameManager.Instance.getStage();
        float damage = (1.0f + 0.1f * stage) * stat.attack + Random.Range(0, stage);

        curruntHP += damage;

    }

    public void monsterDie()
    {
        //GameManager.Instance.addCoin(stat.coin);
        Destroy(this.gameObject);
    }


}
