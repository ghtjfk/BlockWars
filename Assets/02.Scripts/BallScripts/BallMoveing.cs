using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;
public class BallMoveing : MonoBehaviour
{
    Vector3 firstPos, secondPos, gap,firstball;
    public GameObject ballPreview;
    public Rigidbody2D rb;
    public bool isMoving = false;
    int moveSpeed = 250;
    private Coroutine forceResetCoroutine;
    public Text timerText;
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

        if (TurnManager.Instance.turnState != TurnState.PlayerTurn)
            return;
        // 마우스 첫번째 좌표
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.isPause.Equals(false))
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
        secondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        secondPos.z = 0;
        Vector3 dir = (secondPos - firstPos);
        if (dir.magnitude < 0.1f) return;
            gap = dir * SettingsData.sensitivity / 2000;


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

        // 발사 시 타이머 코루틴 시작
        if (forceResetCoroutine != null) StopCoroutine(forceResetCoroutine);
        forceResetCoroutine = StartCoroutine(ForceResetTimer(15f));
    }

    private IEnumerator ForceResetTimer(float delay)
    {
        float remainingTime = delay;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true); // 타이머 텍스트 활성화
        }

        while (remainingTime > 0)
        {
            if (timerText != null)
            {
                // 소수점 없이 정수만 표시하고 싶으면 "F0", 소수점 한자리는 "F1"
                timerText.text = "남은 시간: " + remainingTime.ToString("F1") + "s";
                
                // 3초 미만일 때 텍스트를 빨간색으로 변경 (선택 사항)
                if (remainingTime <= 3f) timerText.color = Color.red;
                else timerText.color = Color.white;
            }

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        if (isMoving)
        {
            ResetBall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottom"))
        {
            ResetBall();
        }
    }

    // 리셋 로직을 별도 함수로 분리 (중복 방지)
    private void ResetBall()
    {
        if (forceResetCoroutine != null)
        {
            StopCoroutine(forceResetCoroutine);
            forceResetCoroutine = null;
        }
        
        if (timerText != null) 
        {
            timerText.text = ""; 
            timerText.gameObject.SetActive(false); // 리셋 시 타이머 숨김
        }
        // 타이머가 돌고 있다면 중지
        if (forceResetCoroutine != null)
        {
            StopCoroutine(forceResetCoroutine);
            forceResetCoroutine = null;
        }

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f; // 회전이 있다면 멈춤
        transform.position = firstball;
        isMoving = false;

        // 기존 게임 로직 실행
        if (ModeSwitcher.Instance.GetCurrentMode())
        {
            int count = GameManager.Instance.getBreakBlockCount();
            StartCoroutine(PlayerManager.Instance.Heal(GameManager.Instance.nowPlayer.attackDamage * count));
        }
        else
        {
            int count = GameManager.Instance.getBreakBlockCount();
            PlayerManager.Instance.setBreakBrickCount(count);
        }

        GameManager.Instance.initBreakBlockCount();
        TurnManager.Instance.NextTurn();

        if (ModeSwitcher.Instance.GetCurrentMode())
        {
            ModeSwitcher.Instance.ForceChangeToBattleMode();
            ModeSwitcher.Instance.SetHealCooldown();
        }
        RespawnBrick.Instance.Respawn();
    }
}
