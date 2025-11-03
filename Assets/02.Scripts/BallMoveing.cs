using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
public class BallMoveing : MonoBehaviour
{

    Vector3 firstPos, secondPos, gap;
    public Rigidbody2D rb;
    public bool isMoving;
    void Start()
    {
        
    }

    void Update()
    {
        Update_GM();
    }
    void Update_GM()
    {
        // 마우스 첫번째 좌표
        if (Input.GetMouseButtonDown(0))
            firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        bool isMouse = Input.GetMouseButton(0);
        if (isMouse)
        {
            secondPos = Camera.main.ScreenToViewportPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            if ((secondPos - firstPos).magnitude < 1) return;
            gap = (secondPos - firstPos).normalized;
            gap = new Vector3(gap.y >= 0 ? gap.x : gap.x >= 0 ? 1 : -1, Mathf.Clamp(gap.y, 0.2f, 1), 0);

        }
        if (Input.GetMouseButtonUp(0))
        {
            Launch(gap);
            firstPos = Vector3.zero;
        }


    }
    public void Launch(Vector3 pos)
    {
        isMoving = true;
        rb.AddForce(pos * 500);
    }
}
