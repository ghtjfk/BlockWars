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
        selectMark.SetActive(false);
        monsterBehaviour = GetComponent<MonsterBehaviour>();
    }

    void OnMouseEnter()
    {
        // 예외처리
        if(selectMark == null) return;

        if (GameManager.Instance.turnState == TurnState.MonsterSelect)
            selectMark.SetActive(true);
    }

    void OnMouseExit()
    {
        // 예외처리
        if (selectMark == null) return;

        if (GameManager.Instance.turnState == TurnState.MonsterSelect)
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
        // deltatime으로 2초로 바꾸고 코루틴 삭제시도
        
        monsterBehaviour.TakeDamage(5);
        Debug.Log("Monster took 5 damage!");

        GameManager.Instance.NextTurn();
        selectMark.SetActive(false);
        //  2초 대기
        yield return new WaitForSeconds(2f);

        Debug.Log("2초 후에 다음 턴으로 전환됨");
    }
}
