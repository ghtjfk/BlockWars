using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private MonsterDataBase monsterDataBase;

    public float isBigGenerator = 0.05f;
    

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
        foreach (var orgstat in monsterDataBase.monsterStats)
        {
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

            if (isBigChace < isBigGenerator)
            {
                monster.transform.localScale *= 1.5f; // 거대 몬스터

            }
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

    Vector3 getPosition (MonsterStat stat, int idx)
    {
        return stat.pos + new Vector3(0.8f * idx, 0, 0);
    }
}
