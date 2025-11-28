using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingPanel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickPauseButton()
    {
        GameManager.Instance.isPause = true;
        pausePanel.SetActive(true);
        SceneManager.UnloadSceneAsync("UIScene");
        Time.timeScale = 0f;
    }

    public void ClickResumeButton()
    {
        GameManager.Instance.isPause = false;
        pausePanel.SetActive(false);
        SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
        Time.timeScale = 1f;
    }

    public void ClickSettingButton()
    {
        settingPanel.SetActive(true);
        SceneManager.UnloadSceneAsync("UIScene");
    }

    public void ClickMainMenuButton()
    {
        GameManager.Instance.isPause = false;
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }

    public void ClickMapIconButton()
    {
        SceneManager.LoadScene("MapScene");
    }
}
