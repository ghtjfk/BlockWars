using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    [SerializeField]
    private MonsterDataBase monsterDataBase = null;
    public int maxCount = 4;
    public bool isMonsterClicked = false;

    List<MonsterBehaviour> monsters = new List<MonsterBehaviour>();
    public List<Vector3> posArray = 
        new List<Vector3>() {
            new Vector3(-1.6f, 3.2f, 0),
            new Vector3(-0.8f, 3.2f, 0),
            new Vector3(0f, 3.2f, 0),
            new Vector3(0.8f, 3.2f, 0),
            new Vector3(1.6f, 3.2f, 0),};
    List<int> posInfo = new List<int>();
    int waitMonsterCount;
    public Clear clear;





    void Start()
    {
        int monsterCount = Random.Range(1,3);
        waitMonsterCount = Random.Range(1,4);



        posInfo = GetRandomNumber(monsterCount);
        for (int idx = 0; idx < monsterCount; idx++)
        {
            monsterSpawn(idx);
            Debug.Log("Monster Spawned");
        }
    }

    void aditionSpawn()
    {
        Vector3 pos = posArray[posArray.Count - 1];
        MonsterStat stat = new MonsterStat();

        foreach (MonsterStat m in monsterDataBase.monsterStats)
        {
            if (m.stage == GameManager.Instance.nowPlayer.stage)
                stat = m;
        }

        MonsterStat cloneStat = CloneStat(stat);

        //if (GameManager.Instance.redMoon < GameManager.Instance.isredMoonGenerator)
        //{
        //    stat.hp *= 1.3f;
        //    stat.attack += 3f;
        //    stat.coin += 10;
        //}
        // prefab이 object로 되어있어서 GameObject로 다운캐스팅
        GameObject monster = (GameObject)Instantiate(cloneStat.monsterPrefab, pos, Quaternion.identity);

        // MonsterBehaviour 초기화 추가
        MonsterBehaviour behaviour = monster.GetComponent<MonsterBehaviour>();
        if (behaviour != null)
        {
            behaviour.Init(stat);
            behaviour.posIndex = posArray.Count -1;
            monsters.Add(behaviour);

        }

    }

    void monsterSpawn(int idx)
    {
        Vector3 pos = posArray[posInfo[idx]];
        List<MonsterStat> stageStats = new List<MonsterStat>();

        foreach (MonsterStat m in monsterDataBase.monsterStats)
        {
            if (m.stage == GameManager.Instance.nowPlayer.stage)
            {
                stageStats.Add(m);
            }
        }

        int randomIndex = Random.Range(0, stageStats.Count);
        MonsterStat orgstat = stageStats[randomIndex];

        MonsterStat stat = CloneStat(orgstat);


        //  붉은 달 이벤트일 경우 강화
        //if (GameManager.Instance.redMoon < GameManager.Instance.isredMoonGenerator)
        //{
        //    stat.hp *= 1.3f;
        //    stat.attack += 3f;
        //    stat.coin += 10;
        //}
        // prefab이 object로 되어있어서 GameObject로 다운캐스팅
        GameObject monster = (GameObject)Instantiate(stat.monsterPrefab, pos, Quaternion.identity);

        // MonsterBehaviour 초기화 추가
        MonsterBehaviour behaviour = monster.GetComponent<MonsterBehaviour>();
        if (behaviour != null)
        {
            behaviour.Init(stat);
            behaviour.posIndex = posInfo[idx];
            monsters.Add(behaviour);
            
        }

    }

    MonsterStat CloneStat(MonsterStat original)
    {
        return new MonsterStat
        {
            id = original.id,
            hp = original.hp,
            attack = original.attack,
            coin = original.coin,
            monsterPrefab = original.monsterPrefab
        };
    }


    public IEnumerator OnMonsterTurnStart()
    {
        yield return new WaitForSeconds(2f);
        // null 제거
        monsters.RemoveAll(m => m == null);

        //  1. 현재 몬스터 목록을 posIndex 순으로 정렬한 새 리스트 생성
        List<MonsterBehaviour> ordered = new List<MonsterBehaviour>(monsters);
        ordered.Sort((a, b) => a.posIndex.CompareTo(b.posIndex));

        //  2. 정렬된 리스트를 순서대로 순회 (앞에 있는 애부터)
        foreach (MonsterBehaviour monster in ordered)
        {
            if (monster == null) continue;
            if (!monsters.Contains(monster)) continue; // 이미 죽은 몬스터 SKIP

            int current = monster.posIndex;

            // 공격
            if (current == 0)
            {
                PlayerManager.Instance.TakeDamage(monster.MonsterAttack());
                yield return new WaitForSeconds(2f);
                continue;
            }

            int next = current - 1;

            bool isBlocked = monsters.Exists(m => m != null && m.posIndex == next);

            if (isBlocked)
            {
                PlayerManager.Instance.TakeDamage(monster.MonsterAttack());
                yield return new WaitForSeconds(2f);
            }
            else
            {
                Vector3 startPos = monster.transform.position;
                float t = 0f;
                while (t < 1f)
                {
                    if (monster == null || !monsters.Contains(monster)) yield break;
                    monster.transform.position = Vector3.Lerp(startPos, posArray[next], t);
                    t += Time.deltaTime;
                    yield return null;
                }

                if (monster == null || !monsters.Contains(monster)) yield break;

                monster.transform.position = posArray[next];
                monster.posIndex = next;
            }
        }

        // 추가 스폰
        if (monsters.Count < 4 && waitMonsterCount > 0)
        {
            aditionSpawn();
            waitMonsterCount--;
        }

        TurnManager.Instance.NextTurn();
    }


    public void RemoveMonster(MonsterBehaviour monster)
    {
        if (monster == null) return;

        monsters.Remove(monster);

        Debug.Log($"Removed {monster.name} from monster list. Remaining: {monsters.Count}");

        // 실제로 씬에서 제거
        Destroy(monster.gameObject);
        // 모든 몬스터가 죽었을 때 스테이지 클리어 처리도 가능
        if (monsters.Count == 0 && waitMonsterCount<=0)
        {
            clear.SetGameClear();
            Debug.Log("All monsters defeated!");
        }   
    }

    // 공격 가능한 슬롯들 (0번부터 우선순위)
    public int GetAttackableSlot()
    {
        // 공격가능한 슬롯 확인


        for (int i = 0; i < 5; i++)
        {
            // 해당 슬롯에 몬스터가 있는지 확인
            if (IsSlotOccupied(i))
                return i; // 이 슬롯에서 공격해도 됨
        }

        // 0~2 슬롯에 아무도 없으면 0으로 지정해 몬스터 이동시키게 함
        return 0;
    }

    // 슬롯 점유 여부
    public bool IsSlotOccupied(int slotIndex)
    {
        foreach (var m in monsters)
        {
            if (m == null) continue;

            if (Vector3.Distance(m.transform.position, posArray[slotIndex]) < 0.1f)
                return true;
        }
        return false;
    }





    List<int> GetRandomNumber(int idx)
    {
        List<int> numbers = new List<int>();

        for(int i = 2; i< posArray.Count; i++)
        {
            numbers.Add(i);
            
        }

        // 셔플 (Fisher-Yates)
        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // UnityEngine.Random
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        // 필요한 개수만 반환
        return numbers.GetRange(0, idx);
    }
}

