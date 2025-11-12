using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHover : MonoBehaviour
{
    public GameObject selectMark;
    MonsterBehaviour monsterBehaviour;

    void Start()
    {
        if (selectMark != null)
            selectMark.SetActive(false);
        monsterBehaviour = GetComponent<MonsterBehaviour>();
    }

    void OnMouseEnter()
    {
        if (GameManager.Instance.turnState != TurnState.MonsterSelect)
            return;
        if (selectMark != null)
            selectMark.SetActive(true);
    }

    void OnMouseExit()
    {
        if (GameManager.Instance.turnState != TurnState.MonsterSelect)
            return;
        if (selectMark != null)
            selectMark.SetActive(false);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.turnState != TurnState.MonsterSelect)
            return;


        StartCoroutine(OnMonsterClicked());
       
    }

    private IEnumerator OnMonsterClicked()
    {
        monsterBehaviour.takeDamage(5);
        Debug.Log("Monster took 5 damage!");

        //  2초 대기
        yield return new WaitForSeconds(2f);
        GameManager.Instance.NextTurn();


        Debug.Log("2초 후에 다음 턴으로 전환됨");
    }
}
