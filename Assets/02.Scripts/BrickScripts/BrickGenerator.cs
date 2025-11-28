using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrickGenerator : MonoBehaviour
{
    Vector3 initPosition = new Vector3(-0.2f, 1.8f, 0);
    Vector3 pos;
    public BrickPool brickPool;
    float xGap = 0.25f;
    float yGap = 0.149f;
    public GameObject mapPosition;
    int stage;
    bool isHealMode;

    void Start()
    {

        // 모드 체크
        isHealMode = ModeSwitcher.Instance.GetCurrentMode();
        // BrickPosition에 있는 map data를 가져옴
        int[][][,] mapData = mapPosition.GetComponent<BrickPosition>().GetAllBrickPosition();


        // stage 정보
        // mapScene에서 stage button 클릭 시 stage 정보를 저장하는 식으로 구현 할 것
        // 준혁이가 보고 구현예정
        stage = GameManager.Instance.nowPlayer.stage;

        int randomIndex = Random.Range(0, mapData[stage].GetLength(0));

        for (int y = 0; y < mapData[stage][randomIndex].GetLength(0); y++)
        {
            for (int x = 0; x < mapData[stage][randomIndex].GetLength(1); x++)
            { // 벽돌이 있는 위치만 벽돌 생성
                // 나중에 다른 벽돌들도 추가할 예정
                // 다른 벽돌들은 2, 3, ...으로 구분
                //따라서 switch문으로 변경 예정
                if (mapData[stage][randomIndex][y, x] == 1)
                {

                    pos = initPosition + new Vector3(x * xGap, -y * yGap, 0);

                    GameObject brick = brickPool.GetBrick(isHealMode);
                    brick.transform.position = pos;
                    brick.transform.rotation = Quaternion.identity;
                    brick.SetActive(true);
                }
            }
        }
    }

}

