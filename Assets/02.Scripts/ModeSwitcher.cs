using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class ModeSwitcher : Singleton<ModeSwitcher>
{
    public Image currentImage;
    public Sprite battleOn;
    public Sprite healOn;
    private bool isHealMode = false;
    int HealCooldown = 0;


    public GameObject attackBrickPrefab;
    public GameObject healBrickPrefab;
    public GameObject ball;

    public void OneButtonToggle()
    {
        BallMoveing ballScript = ball.GetComponent<BallMoveing>();
        if (ballScript.isMoving || TurnManager.Instance.turnState != TurnState.PlayerTurn) return;
        if (HealCooldown > 0) return;
        isHealMode = !isHealMode;

        GameObject[] attackBricks = GameObject.FindGameObjectsWithTag("AttackBrick");
        GameObject[] healBricks = GameObject.FindGameObjectsWithTag("HealBrick");
        List<Vector3> oldPositions = new List<Vector3>();

        if (!isHealMode)
        {
            currentImage.sprite = battleOn;

            foreach (GameObject brick in healBricks)
            {
                oldPositions.Add(brick.transform.position);
                Destroy(brick);
            }

            foreach (Vector3 pos in oldPositions)
            {
                GameObject newBrick = Instantiate(attackBrickPrefab, pos, Quaternion.identity);
                newBrick.tag = "AttackBrick";
            }
        }
        else
        {
            currentImage.sprite = healOn;

            foreach (GameObject brick in attackBricks)
            {
                oldPositions.Add(brick.transform.position);
                Destroy(brick);
            }

            foreach (Vector3 pos in oldPositions)
            {
                GameObject newBrick = Instantiate(healBrickPrefab, pos, Quaternion.identity);
                newBrick.tag = "HealBrick";
            }
        }
    }

    public void ForceChangeToBattleMode()
    {
        // 강제 전투 모드로 복귀 (조건 무시)
        if (isHealMode)
        {
            isHealMode = false;
            currentImage.sprite = battleOn;

            GameObject[] healBricks = GameObject.FindGameObjectsWithTag("HealBrick");
            List<Vector3> oldPositions = new List<Vector3>();

            foreach (GameObject brick in healBricks)
            {
                oldPositions.Add(brick.transform.position);
                Destroy(brick);
            }

            foreach (Vector3 pos in oldPositions)
            {
                GameObject newBrick = Instantiate(attackBrickPrefab, pos, Quaternion.identity);
                newBrick.tag = "AttackBrick";
            }

            Debug.Log("강제 전투 모드 복귀 완료!");
        }
    }




    public bool GetCurrentMode()
    {
        return isHealMode;
    }

    public void DecreaseCooldown()
    {
        if (HealCooldown > 0)
            HealCooldown--;

        Debug.Log("Heal Cooldown: " + HealCooldown);
    }

    public void InitMode()
    {
        isHealMode = false;
        currentImage.sprite = battleOn;
        HealCooldown = 0;
    }
    public void SetHealCooldown()
    {
        HealCooldown = 3;
    }
}