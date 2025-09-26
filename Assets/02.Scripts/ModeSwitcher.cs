using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonToggle : MonoBehaviour
{
    public Image currentImage;
    public Sprite battleOn;
    public Sprite healOn;
    public bool isOn = true;

    public GameObject attackBrickPrefab;
    public GameObject healBrickPrefab;
    private GameObject currentBrick;


    public void OneButtonToggle()
    {
        isOn = !isOn;

        GameObject[] attackBricks = GameObject.FindGameObjectsWithTag("AttackBrick");
        GameObject[] healBricks = GameObject.FindGameObjectsWithTag("HealBrick");
        List<Vector3> oldPositions = new List<Vector3>();

        if (!isOn)
        {
            currentImage.sprite = battleOn;
            BattleMethod();

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
            HealMethod();

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


    public void BattleMethod()
    {
        Debug.Log("BATTLE Event");
    }

    public void HealMethod()
    {
        Debug.Log("HEAL Event");
    }
}