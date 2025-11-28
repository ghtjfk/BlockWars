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
        // 抗寇贸府
        if(selectMark == null) return;

        if (TurnManager.Instance.turnState == TurnState.MonsterSelect &&
             !MonsterManager.Instance.isMonsterClicked)
            selectMark.SetActive(true);

        monsterHPText.text = GetHPText();

    }

    void OnMouseExit()
    {
        // 抗寇贸府
        if (selectMark == null) return;

        if (TurnManager.Instance.turnState == TurnState.MonsterSelect &&
            !MonsterManager.Instance.isMonsterClicked)
            selectMark.SetActive(false);

        monsterHPText.text = "";
    }

    void OnMouseDown()
    {
        if (TurnManager.Instance.turnState != TurnState.MonsterSelect)
            return;


        OnMonsterClicked();


    }

    private void OnMonsterClicked()
    {
        PlayerManager.Instance.animator.SetTrigger("doAttack");
        selectMark.SetActive(false);
        MonsterManager.Instance.isMonsterClicked = true;


        monsterBehaviour.TakeDamage(5);
        Debug.Log("Monster took 5 damage!");

        if (monsterBehaviour.GetCurrentHP() <= 0)
        {
            MonsterManager.Instance.isMonsterClicked = false;

            monsterBehaviour.MonsterDie();
            return;
        }

        TurnManager.Instance.startWaitAndNextTurn(2f);
        MonsterManager.Instance.isMonsterClicked = false;


    }

    string GetHPText()
    {
        string currentHP = monsterBehaviour.GetCurrentHP().ToString();

        return currentHP + " / " + MaxHP.ToString();
    }
}
