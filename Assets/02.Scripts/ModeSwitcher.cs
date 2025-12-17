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


    public GameObject attackBrickPrefab;
    public GameObject healBrickPrefab;
    public GameObject ball;
    public Text cooldownText;

    public void OneButtonToggle()
    {
        BallMoveing ballScript = ball.GetComponent<BallMoveing>();
        if (ballScript.isMoving || TurnManager.Instance.turnState != TurnState.PlayerTurn) return;
        if (GameManager.Instance.healCooldown > 0)
        {
            StartCoroutine(onCooldownUI());
            return;
        }
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
        // ���� ���� ���� ���� (���� ����)
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

            Debug.Log("���� ���� ��� ���� �Ϸ�!");
        }
    }




    public bool GetCurrentMode()
    {
        return isHealMode;
    }

    public void DecreaseCooldown()
    {
        if (GameManager.Instance.healCooldown > 0)
            GameManager.Instance.healCooldown--;

        Debug.Log("Heal Cooldown: " + GameManager.Instance.healCooldown);
    }

    public void SetHealCooldown()
    {
        GameManager.Instance.healCooldown = 3;
    }

    public IEnumerator onCooldownUI()
    {
        cooldownText.text = $"{GameManager.Instance.healCooldown}턴 뒤에 사용가능!";
        cooldownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        cooldownText.gameObject.SetActive(false);

    }
}