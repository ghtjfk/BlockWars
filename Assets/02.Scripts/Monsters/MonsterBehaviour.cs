using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{

    MonsterStat stat;
    float curruntHP;
    Vector3 pos;
    public int posIndex;

    public void Init(MonsterStat stat)
    {
        this.stat = stat;
        curruntHP = stat.hp;
    }

    public float MonsterAttack()
    {
        int stage = GameManager.Instance.getStage();
        float damage = (1.0f + 0.1f * stage) * stat.attack + Random.Range(0, stage);

        return damage;
    }

    public void TakeDamage(float damage)
    {
        curruntHP -= damage;
        Debug.Log($"Monster took {damage} damage, current HP: {curruntHP}");
        if (curruntHP <= 0)
        {
            MonsterDie();
        }
 
    }

    public void MonsterHeal()
    {
        int stage = GameManager.Instance.getStage();
        float damage = (1.0f + 0.1f * stage) * stat.attack + Random.Range(0, stage);

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

    }


    public IEnumerator MonsterAction()
    {
        int attackSlot = MonsterManager.Instance.GetAttackableSlot();
        Vector3 targetPos = MonsterManager.Instance.posArray[attackSlot];

        // 이미 공격 슬롯에 있으면 바로 공격
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            yield return AttackRoutine();
            yield break;
        }

        // 아니면 해당 슬롯으로 이동
        yield return MoveTo(targetPos);

        // 도착 후 공격
        yield return AttackRoutine();
    }

    IEnumerator MoveTo(Vector3 target)
    {
        // 부드럽게 이동 (0.5초 정도)
        float t = 0f;

        Vector3 start = transform.position;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f; // 속도
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }

    IEnumerator AttackRoutine()
    {
        float damage = MonsterAttack();
        PlayerManager.Instance.TakeDamage(damage);

        Debug.Log($"Monster attacked! damage: {damage}");

        yield return new WaitForSeconds(1f);
    }

}
