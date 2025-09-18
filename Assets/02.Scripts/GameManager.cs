using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Button startButton;
    public int hp = 5;

    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/

    void Start()
    {
        // ��ư Ŭ�� �� OnStartButtonClick ����
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("MapScene");
    }

    //����Ʈ ��â~
}
