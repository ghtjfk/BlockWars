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
        // ��ư Ŭ�� �� OnStartButtonClick ����
        startButton.onClick.AddListener(OnStartButtonClick);
    }


    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("MapScene");
    }
}
