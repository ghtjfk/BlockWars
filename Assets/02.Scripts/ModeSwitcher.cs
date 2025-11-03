using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class ModeSwitcher : MonoBehaviour
{
    public static ModeSwitcher Instance;
    public Image currentImage;
    public Sprite battleOn;
    public Sprite healOn;
    private bool isHealMode = false;


    public GameObject attackBrickPrefab;
    public GameObject healBrickPrefab;
    private GameObject currentBrick;
    public GameObject ball;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OneButtonToggle()
    {
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




    public bool GetCurrentMode()
    {
        return isHealMode;
    }
}