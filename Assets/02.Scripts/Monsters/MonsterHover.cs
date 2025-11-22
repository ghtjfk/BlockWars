using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHover : MonoBehaviour
{
    public GameObject selectMark;
    public Text monsterHPText;
    MonsterBehaviour monsterBehaviour;
    float MaxHP;

    void Start()
    {
        selectMark.SetActive(false);
        monsterBehaviour = GetComponent<MonsterBehaviour>();
        MaxHP = monsterBehaviour.stat.hp;
    }

   

    void OnMouseEnter()
    {
        // 예외처리
        if(selectMark == null) return;

        if (GameManager.Instance.turnState == TurnState.MonsterSelect &&
             !MonsterManager.Instance.isMonsterClicked)
            selectMark.SetActive(true);

        monsterHPText.text = GetHPText();

    }

    void OnMouseExit()
    {
        // 예외처리
        if (selectMark == null) return;

        if (GameManager.Instance.turnState == TurnState.MonsterSelect &&
            !MonsterManager.Instance.isMonsterClicked)
            selectMark.SetActive(false);

        monsterHPText.text = "";
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.turnState != TurnState.MonsterSelect)
            return;


        StartCoroutine(OnMonsterClicked());


    }

    private IEnumerator OnMonsterClicked()
    {
        selectMark.SetActive(false);
        MonsterManager.Instance.isMonsterClicked = true;


        monsterBehaviour.TakeDamage(20);
        Debug.Log("Monster took 5 damage!");

        if (monsterBehaviour.GetCurrentHP() <= 0)
        {
            MonsterManager.Instance.isMonsterClicked = false;
            monsterBehaviour.MonsterDie();
        }
        //  2초 대기
        yield return new WaitForSeconds(2f);

        GameManager.Instance.NextTurn();
        MonsterManager.Instance.isMonsterClicked = false;

 

        Debug.Log("2초 후에 다음 턴으로 전환됨");
    }

    string GetHPText()
    {
        string currentHP = monsterBehaviour.GetCurrentHP().ToString();

        return currentHP + " / " + MaxHP.ToString();
    }
}
