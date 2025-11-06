using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBrick : Singleton<RespawnBrick>
{


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

    private List<Respawninfo> respawnList = new List<Respawninfo>();

    public void NextTrun()
    { //다음 턴마다 벽돌 리스폰 처리
        for (int i = respawnList.Count - 1; i >= 0; i--)
        {
            respawnList[i].turnLeft--;
            if (respawnList[i].turnLeft <= 0)
            {
                GameObject brick = BrickPool.Instance.GetBrick(ModeSwitcher.Instance.GetCurrentMode());
                brick.transform.position = respawnList[i].position;
                brick.transform.rotation = Quaternion.identity;
                brick.SetActive(true);
                respawnList.RemoveAt(i);
            }
        }
    }

    public void AddRespawn(Vector3 pos)
    {//벽돌 리스폰 정보 추가
        int turns = Random.Range(2, 4);
        respawnList.Add(new Respawninfo(pos, turns));
    }
}
