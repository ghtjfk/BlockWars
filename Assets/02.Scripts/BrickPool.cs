using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPool : MonoBehaviour
{
    public static BrickPool Instance;
    public GameObject CommonBrickPrefab, HealBrickPrefab;
    public int poolSize = 100;

    // 블럭 큐 (새로운 블럭을 만들때마다 Queue 만들어서 저장)
    private Queue<GameObject> commonPool = new Queue<GameObject>();
    private Queue<GameObject> healPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            // poolSize 만큼 미리 생성 해두기
            // 근데 새로운 블럭이 생길때마다 큐에 poolSize 만큼 생성? 너무 메모리 낭비같음
            // -> 일단은 이렇게 하고 나중에 최적화 필요
            GameObject commonBrick = Instantiate(CommonBrickPrefab);
            commonBrick.SetActive(false);
            commonPool.Enqueue(commonBrick);

            GameObject healBrick = Instantiate(HealBrickPrefab);
            healBrick.SetActive(false);
            healPool.Enqueue(healBrick);
        }
    }
    public GameObject GetBrick(bool isHeal)
    {   // 힐 모드에 따른 블럭 생성
        if (isHeal)
        {
            if (healPool.Count > 0)
            {
                GameObject brick = healPool.Dequeue();
                brick.SetActive(true);
                return brick;
            }
            else
            { //Ǯ�� ������ ������ ���� ����
                GameObject brick = Instantiate(HealBrickPrefab);
                return brick;
            }
        }
        else
        {
            if (commonPool.Count > 0)
            {
                GameObject brick = commonPool.Dequeue();
                brick.SetActive(true);
                return brick;
            }
            else
            { //Ǯ�� ������ ������ ���� ����
                GameObject brick = Instantiate(CommonBrickPrefab);
                return brick;
            }
        }
    }

    public void ReturnBrick(GameObject brick, bool isHeal)
    {// 블럭이 공과 충돌하여 깨지면 리턴

        brick.SetActive(false);
        GameManager.Instance.increaseBreakBlockCount();
  
        if ( isHeal )
        {
            healPool.Enqueue(brick);
        }
        else
            commonPool.Enqueue(brick);
    }

}
