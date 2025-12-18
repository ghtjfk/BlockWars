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
             !MonsterManager.Instance.isMonsterClicked && !GameManager.Instance.isPause
             && !TurnManager.Instance.isTurnChanging)
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

        if (!GameManager.Instance.isPause && !TurnManager.Instance.isTurnChanging)
        {
            OnMonsterClicked();
            MonsterManager.Instance.UpdateBreakBrickUI();
        }

        }

    private void OnMonsterClicked()
    {
        PlayerManager.Instance.animator.SetTrigger("doAttack");
        selectMark.SetActive(false);
        MonsterManager.Instance.isMonsterClicked = true;


        monsterBehaviour.TakeDamage(GameManager.Instance.nowPlayer.attackDamage *PlayerManager.Instance.getBreakBrickCount());
        Debug.Log($"Monster took {GameManager.Instance.nowPlayer.attackDamage * PlayerManager.Instance.getBreakBrickCount()} damage!");

        if (monsterBehaviour.GetCurrentHP() <= 0)
        {
            MonsterManager.Instance.isMonsterClicked = false;

            monsterBehaviour.MonsterDie();
            return;
        }

        PlayerManager.Instance.initBreakBrickCount();
        TurnManager.Instance.startWaitAndNextTurn(2f);
        MonsterManager.Instance.isMonsterClicked = false;
        

    }

    string GetHPText()
    {
        string currentHP = monsterBehaviour.GetCurrentHP().ToString();

        return currentHP + " / " + MaxHP.ToString();
    }
}
