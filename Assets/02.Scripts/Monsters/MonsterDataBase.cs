using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class MonsterDataBase : ScriptableObject
{
    public List<MonsterStat> monsterStats = new List<MonsterStat>();



    //List에있는 몬스터 데이터를 id로 찾아서 변환
    public MonsterStat GetMonsterData(string id)
    {
        foreach (var stat in monsterStats)
        {
            if (stat.id == id)
            {
                return stat;
            }

        }
        return null;
    }
}
