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

    public void ClickMainMenuButton()
    {
        int currentSlot = GameManager.Instance.nowSlot;
        GameManager.Instance.DeleteSaveFile(currentSlot);
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }

}
