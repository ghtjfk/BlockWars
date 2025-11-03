using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI level;
    public TextMeshProUGUI coin;

    public GameObject[] item;
    
    void Start()
    {
        name.text += GameManager.Instance.nowPlayer.name;
        level.text += GameManager.Instance.nowPlayer.level.ToString();
        coin.text += GameManager.Instance.nowPlayer.coin.ToString();
        ItemSetting(GameManager.Instance.nowPlayer.item);
    }

    public void LevelUp()
    {
        GameManager.Instance.nowPlayer.level++;
        level.text = "레벨 : " + GameManager.Instance.nowPlayer.level.ToString();
    }

    public void CoinUp()
    {
        GameManager.Instance.nowPlayer.coin += 100;
        coin.text = "코인 : " + GameManager.Instance.nowPlayer.coin.ToString();
    }

    public void Save()
    {
        GameManager.Instance.SaveData();
    }

    public void ItemSetting(int number)
    {
        for (int i = 0;i< item.Length; i++)
        {
            if(number == i)
            {
                item[i].SetActive(true);
                GameManager.Instance.nowPlayer.item = number;
            }
            else
            {
                item[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        
    }
}
