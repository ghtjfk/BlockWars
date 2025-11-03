using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPool : MonoBehaviour
{
    public static BrickPool Instance;
    public GameObject CommonBrickPrefab, HealBrickPrefab;
    public int poolSize = 100;
    private Queue<GameObject> commonPool = new Queue<GameObject>();
    private Queue<GameObject> healPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject commonBrick = Instantiate(CommonBrickPrefab);
            commonBrick.SetActive(false);
            commonPool.Enqueue(commonBrick);

            GameObject healBrick = Instantiate(HealBrickPrefab);
            healBrick.SetActive(false);
            healPool.Enqueue(healBrick);
        }
    }
    public GameObject GetBrick(bool isHeal)
    {
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
    {

        brick.SetActive(false);

        if ( isHeal )
        {
            healPool.Enqueue(brick);
        }
        else
            commonPool.Enqueue(brick);
    }

}
