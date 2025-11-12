using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    [SerializeField]
    private MonsterDataBase monsterDataBase = null;
    public float isBigGenerator = 0.05f;

    List<MonsterBehaviour> monsters = new List<MonsterBehaviour>();




    void Start()
    {
        for (int idx = 0; idx < Random.Range(1, 5); idx++)
        {
            monsterSpawn(idx);
            Debug.Log("Monster Spawned");
        }
    }

    void monsterSpawn(int idx)
    {
        List<MonsterStat> stageStats = new List<MonsterStat>();

        foreach (MonsterStat m in monsterDataBase.monsterStats)
        {
            if (m.stage == GameManager.Instance.getStage())
            {
                stageStats.Add(m);
            }
        }

        int randomIndex = Random.Range(0, stageStats.Count);
        MonsterStat orgstat = stageStats[randomIndex];

        MonsterStat stat = CloneStat(orgstat);



        Vector3 pos = getPosition(stat, idx);

        float isBigChace = Random.value;


        //  붉은 달 이벤트일 경우 강화
        if (GameManager.Instance.redMoon < GameManager.Instance.isredMoonGenerator)
        {
            stat.hp *= 1.3f;
            stat.attack += 3f;
            stat.coin += 10;
        }

        if (isBigChace < isBigGenerator)
        {
            stat.hp *= 1.5f;
            stat.attack += 3f;
            stat.coin += 20;
            pos += new Vector3(0, 0.1f, 0);
        }
        // prefab이 object로 되어있어서 GameObject로 다운캐스팅
        GameObject monster = (GameObject)Instantiate(stat.monsterPrefab, pos, Quaternion.identity);

        // MonsterBehaviour 초기화 추가
        MonsterBehaviour behaviour = monster.GetComponent<MonsterBehaviour>();
        if (behaviour != null)
        {
            behaviour.init(stat);
            monsters.Add(behaviour);
        }


        if (isBigChace < isBigGenerator)
        {
            monster.transform.localScale *= 1.5f; // 거대 몬스터

        }

    }

    MonsterStat CloneStat(MonsterStat original)
    {
        return new MonsterStat
        {
            id = original.id,
            hp = original.hp,
            attack = original.attack,
            pos = original.pos,
            coin = original.coin,
            monsterPrefab = original.monsterPrefab
        };
    }

    Vector3 getPosition(MonsterStat stat, int idx)
    {
        return stat.pos + new Vector3(0.8f * idx, 0, 0);
    }

    public IEnumerator OnMonsterTurnStart()
    {

        Debug.Log("호출");

        foreach (MonsterBehaviour monster in monsters)
        {
            float damage = monster.monsterAttack();
            PlayerManager.Instance.TakeDamage(damage);
            Debug.Log($"Monster attacked for {damage} damage!");
            // 2초 대기
            yield return new WaitForSeconds(2f);
        }

        GameManager.Instance.NextTurn();

        Debug.Log("2초가 지났습니다. 다음 턴으로 진행합니다!");
    }

    public void RemoveMonster(MonsterBehaviour monster)
    {
        if (monsters.Contains(monster))
        {
            monsters.Remove(monster);
            Debug.Log($"Removed {monster.name} from monster list. Remaining: {monsters.Count}");
        }

        // 모든 몬스터가 죽었을 때 스테이지 클리어 처리도 가능
        if (monsters.Count == 0)
        {
            Debug.Log("All monsters defeated!");
            //GameManager.Instance.StageClear();
        }
    }

}

