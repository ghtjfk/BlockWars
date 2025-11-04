using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrickPosition : MonoBehaviour
{

    //stage, random, 블럭위치
    int[][][,] allMaps;
    private void Start()
    {

        //벽돌 설치할 포지션, 벽돌 종류에따라 1, 2, 3 ....으로 배치
        //최대 (9,y)로 배치

        int[,] zeroMap = new int[,]
        { //stage가 1부터 시작이니 0번째는 빈맵
            {0 }
        };

        int[,] map1_1 = new int[,]
        {

            {1,1,1,1,1,1,1,1,1 }
        };

        int[,] map1_2 = new int[,]
 {

            {1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1 }
 };

        int[,] map1_3 = new int[,]
 {

            {1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1 }
 };

        int[,] map2_1 = new int[,]
        {
            {1,1,1,1,1,1,1,1,1 },
            {0,1,1,1,1,1,1,1,0 },
            {0,0,1,1,1,1,1,0,0 }
        };

        int[,] map2_2 = new int[,]
        {
            {0,0,1,1,1,1,1,0,0 },
            {0,1,1,1,1,1,1,1,0 },
            {1,1,1,1,1,1,1,1,1 }
        };

        int[,] map2_3 = new int[,]
        {
            {1,0,0,1,1,1,0,0,1 },
            {0,1,1,1,1,1,1,1,0 },
            {1,1,1,1,1,1,1,1,1 }
        };



        allMaps = new int[][][,]
        {
            new int [][,] {zeroMap},
            new int [][,] {map1_1, map1_2, map1_3}



        };

    }


    public int[][][,] GetAllBrickPosition()
    {
        return allMaps;
    }
}



