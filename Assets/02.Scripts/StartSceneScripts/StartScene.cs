using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button newStartButton;
    public Button continueButton;

    void Start()
    {
        bool hasSave = false;
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(GameManager.Instance.path + $"{i}"))
            {
                hasSave = true;
                break;
            }
        }
        continueButton.gameObject.SetActive(hasSave);
    }


    public void GoNewStart()
    {
        GameManager.Instance.newStart = true;
        SceneManager.LoadScene("Select");
    }

    public void GoContineStart()
    {
        GameManager.Instance.newStart = false;
        SceneManager.LoadScene("Select");
    }
}