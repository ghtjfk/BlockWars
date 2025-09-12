using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Button startButton;

    void Start()
    {
        // ��ư Ŭ�� �� OnStartButtonClick ����
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    //����Ʈ ��â~
}
