using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;


    public void SetGameOver()
    {
        gameOverPanel.SetActive(true);
        GameManager.Instance.isGameOver = true;
        SceneManager.UnloadSceneAsync("UIScene");
        Time.timeScale = 0f;

    }
    public void ClickRestartButton()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MapScene");
        Time.timeScale = 1f;
        
        // restartÇÔ¼ö
    }

    public void ClickMainMenuButton()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }

}
