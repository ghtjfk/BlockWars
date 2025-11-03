//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerManager : MonoBehaviour
//{

//    public static PlayerManager instance;

//    float maxHP = 100;
//    float curruntHP;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//    void Start()
//    {
//        curruntHP = maxHP;
//        HPUIManager.instance.UpdateHPbar(curruntHP, maxHP);
//    }

//    void TakeDamage(float curruntHP, float damage)
//    {
//        curruntHP = Mathf.Min(curruntHP - damage, 0);
//        HPUIManager.instance.UpdateHPbar(curruntHP, maxHP);
//    }

//    void Heal(float curruntHP, float healAmount)
//    {
//        curruntHP = Mathf.Max(curruntHP + healAmount, maxHP);
//        HPUIManager.instance.UpdateHPbar(curruntHP, maxHP);
//    }
//}
