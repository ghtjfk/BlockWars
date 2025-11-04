using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


public class Select : MonoBehaviour
{
    public GameObject creat;
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI newPlayerName;

    bool[] savefile = new bool[3];

    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        // 있다면 문구를 "슬롯1" -> "플레이어 이름"으로 바꾸려고.
        for(int i = 0; i < 3; i++)
        {
            if(File.Exists(GameManager.Instance.path + $"{i}"))
            {
                savefile[i] = true;
                GameManager.Instance.nowSlot = i; // 슬롯 i번째로 장전
                GameManager.Instance.LoadData(); // 불러오기
                slotText[i].text = GameManager.Instance.nowPlayer.name; // 슬롯에 이름 표기
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        // 문구를 잘 바꿨다면 nowSlot과 nowPlayer는 원상복귀.
        // 위의 for문은 그저 문구를 바꾸기 위함이었기 때문.
        GameManager.Instance.DataClear();
    }

    void Update()
    {
        
    }

    public void Slot(int number)
    {
        // 몇번째 슬롯에 대한 건지 설정
        GameManager.Instance.nowSlot = number;

        if (GameManager.Instance.newStart)  // 새로운 시작일 경우 해당 경로의 파일 삭제
        {
            string filePath = GameManager.Instance.path + $"{number}";
            File.Delete(filePath);
            savefile[number] = false;
        }

        if (savefile[number]) // 2. 저장된 데이터가 있을때 => 불러오기해서 게임씬으로 넘어감.
        {
            GameManager.Instance.LoadData();
            GoGame();
        }
        else // 1. 저장된 데이터가 없을때
        {
            Creat();
        }
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        // 저장된 데이터가 없다면, 방금 입력했던 플레이어 이름을 덮어씌워라.
        if (!savefile[GameManager.Instance.nowSlot])
        {
            GameManager.Instance.nowPlayer.name = newPlayerName.text;
            GameManager.Instance.SaveData();
        }
        
        SceneManager.LoadScene("MapScene");
    }
}
