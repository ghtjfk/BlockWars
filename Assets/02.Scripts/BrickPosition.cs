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

        //���� ��ġ�� ������, ���� ���������� 1, 2, 3���� ��ġ
        //�ִ� (8,y)�� ��ġ

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
