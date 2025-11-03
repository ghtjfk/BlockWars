using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPool : MonoBehaviour
{
    public static BrickPool Instance;
    public GameObject CommonBrickPrefab;
    public int poolSize = 100;
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject brick = Instantiate(CommonBrickPrefab);
            brick.SetActive(false);
            pool.Enqueue(brick);
        }
    }
    public GameObject GetBrick()
    {
        //벽돌 꺼내기
        if (pool.Count > 0)
        {
            GameObject brick = pool.Dequeue();
            brick.SetActive(true);
            return brick;
        }
        else
        { //풀에 벽돌이 없으면 새로 생성
            GameObject brick = Instantiate(CommonBrickPrefab);
            return brick;
        }
    }

    public void ReturnBrick(GameObject brick)
    {
        brick.SetActive(false);
        pool.Enqueue(brick);
    }

}
