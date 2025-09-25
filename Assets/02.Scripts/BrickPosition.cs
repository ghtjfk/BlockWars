using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrickPosition : MonoBehaviour
{

    int[][,] allMaps;
    private void Start()
    {

        //벽돌 설치할 포지션, 벽돌 종류에따라 1, 2, 3으로 배치
        //최대 (8,y)로 배치

        int[,] zeroMap = new int[,]
        {
            {0 }
        };

        int[,] brickPosition = new int[,]
        {

            {1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1 }
        };

        allMaps = new int[][,]
        {
            zeroMap,
            brickPosition

        };

    }

    public int[][,] GetAllBrickPosition()
    {
        return allMaps;
    }


}
