using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBrick : MonoBehaviour
{

    public static RespawnBrick Instance;

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public class Respawninfo
    {
        //벽돌 리스폰 정보 클래스
        public Vector3 position;
        public int turnLeft;
        public Respawninfo(Vector3 pos, int turn)
        {
            position = pos;
            turnLeft = turn;
        }
    }

    private List<Respawninfo> respawnQueue = new List<Respawninfo>();

    public void NextTrun()
    { //다음 턴마다 벽돌 리스폰 처리
        for (int i = respawnQueue.Count - 1; i >= 0; i--)
        {
            respawnQueue[i].turnLeft--;
            if (respawnQueue[i].turnLeft <= 0)
            {
                GameObject brick = BrickPool.Instance.GetBrick(ModeSwitcher.Instance.GetCurrentMode());
                brick.transform.position = respawnQueue[i].position;
                brick.transform.rotation = Quaternion.identity;
                brick.SetActive(true);
                respawnQueue.RemoveAt(i);
            }
        }
    }

    public void AddRespawn(Vector3 pos)
    {//벽돌 리스폰 정보 추가
        int turns = Random.Range(2, 4);
        respawnQueue.Add(new Respawninfo(pos, turns));
    }
}
