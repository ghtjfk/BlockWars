using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrickGenerator : MonoBehaviour
{
    Vector3 initPosition = new Vector3(-0.2f, 1.8f, 0);
    Vector3 pos;
    public BrickPool brickPool;
    float xGap = 0.3f;
    float yGap = 0.3f;
    public GameObject mapPosition;
    int stage;

    void Start()
    {

        int[][,] mapData = mapPosition.GetComponent<BrickPosition>().GetAllBrickPosition();

        stage = 1;

        for (int y = 0; y < mapData[stage].GetLength(0); y++)
        {
            for (int x = 0; x < mapData[stage].GetLength(1); x++)
            {
                if (mapData[stage][y, x] == 1)
                {


                    pos = initPosition + new Vector3(x * xGap, -y * yGap, 0);

                    // 오브젝트 풀에서 벽돌 가져오기
                    GameObject brick = brickPool.GetBrick();
                    brick.transform.position = pos;
                    brick.transform.rotation = Quaternion.identity;
                    brick.SetActive(true);
                }
            }
        }
    }

}

