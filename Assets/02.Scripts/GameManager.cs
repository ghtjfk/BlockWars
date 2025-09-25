using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Button startButton;

    

    void Start()
    {
        // 버튼 클릭 시 OnStartButtonClick 실행
        //startButton.onClick.AddListener(OnStartButtonClick);
        LoadBrickMap();
        LoadHP();
    }
    

    //private void OnStartButtonClick()
    //{
    //    SceneManager.LoadScene("GameScene");
    //}
    private void LoadBrickMap()
    {
        SceneManager.LoadScene("BrickScene", LoadSceneMode.Additive);
    }

    void LoadHP()
    {
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }
}
