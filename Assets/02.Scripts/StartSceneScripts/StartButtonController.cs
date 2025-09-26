using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        // 버튼 클릭 시 OnStartButtonClick 실행
        startButton.onClick.AddListener(OnStartButtonClick);
    }


    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("MapScene");
    }
}
