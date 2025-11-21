using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickPauseButton()
    {
        GameManager.Instance.isPause = true;
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClickResumeButton()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.isPause = false;
    }

    public void ClickSettingButton()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;    // 세팅창이 없어서 아직 연결 X
    }

    public void ClickMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
        GameManager.Instance.isPause = false;
    }
}
