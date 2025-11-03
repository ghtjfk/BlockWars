using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
public class BallMoveing : MonoBehaviour
{
    Vector3 firstPos, secondPos, gap,firstball;
    public GameObject ballPreview;
    public Rigidbody2D rb;
    public bool isMoving = false;
    int moveSpeed=250;
    void Start()
    {
        firstball = this.transform.position;
    }

    void Update()
    {
        if (isMoving){}
        else {Update_GM();}
    }
    void Update_GM()
{
    // 마우스 첫번째 좌표
    if (Input.GetMouseButtonDown(0))
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // 시작 위치가 Background 오브젝트 위인지 체크
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider == null || !hit.collider.CompareTag("Background"))
            return; // Background가 아니면 아무것도 안함

        firstPos = mousePos;
    }

    bool isMouse = Input.GetMouseButton(0);
    if (isMouse && firstPos != Vector3.zero) // 시작이 Background에서만 진행
    {
        secondPos = Camera.main.ScreenToViewportPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        if ((secondPos - firstPos).magnitude < 1) return;
        gap = (secondPos - firstPos).normalized;
        gap = new Vector3(gap.y >= 0 ? gap.x : gap.x >= 0 ? 1 : -1, Mathf.Clamp(gap.y, 0.2f, 1), 0);
        ballPreview.transform.position = Physics2D.CircleCast(
            new Vector2(Mathf.Clamp(transform.position.x, -0.425f, 2.425f), -4.5f), 
            0.1f, 
            gap, 
            10000, 
            1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Block")
        ).centroid;
    }

    if (Input.GetMouseButtonUp(0) && firstPos != Vector3.zero)
    {
        Launch(gap);
        firstPos = Vector3.zero;
    }

    ballPreview.SetActive(isMouse && firstPos != Vector3.zero);
}

    public void Launch(Vector3 pos)
    {
        isMoving = true;
        rb.AddForce(pos.normalized * moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottom"))
        {
            rb.velocity = Vector3.zero;
            transform.position = firstball;
            RespawnBrick.Instance.NextTrun();
            isMoving = false;
            GameManager.Instance.initBreakBlockCount();
        }
    }
}
