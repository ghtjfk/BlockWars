using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    MonsterSelect,
    MonsterTurn
}

public class TurnManager : Singleton<TurnManager>
{


    public TurnState turnState = TurnState.PlayerTurn;

    public void NextTurn()
    {
        switch (turnState)
        {
            case TurnState.PlayerTurn:
                if (ModeSwitcher.Instance.GetCurrentMode())
                {
                    turnState = TurnState.MonsterTurn;
                    StartCoroutine(MonsterManager.Instance.OnMonsterTurnStart());
                    break;
                }
                turnState = TurnState.MonsterSelect;
                ModeSwitcher.Instance.DecreaseCooldown();
                break;
            case TurnState.MonsterSelect:
                turnState = TurnState.MonsterTurn;
                //코루틴은 아래 방식으로 호출해야함
                StartCoroutine(MonsterManager.Instance.OnMonsterTurnStart());
                break;
            case TurnState.MonsterTurn:
                turnState = TurnState.PlayerTurn;
                break;
        }
    }

    public void initTurn()
    {
        turnState = TurnState.PlayerTurn;
    }

    public IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    
    
    public IEnumerator waitAndNextTurn(float time)
    {
        NextTurn();
        yield return new WaitForSeconds(time);
    }

    public IEnumerator deadMonsterSequence(float time, GameObject deadmonster)
    {

        yield return new WaitForSeconds(time);
        Destroy(deadmonster);
        NextTurn();


    }

    public void startWait(float time)
    {
        // this.StartCoroutine()을 사용하여 GameManager 오브젝트 위에서 코루틴을 시작합니다.
        StartCoroutine(wait(time));
    }



    public void startWaitAndNextTurn(float time)
    {
        // this.StartCoroutine()을 사용하여 GameManager 오브젝트 위에서 코루틴을 시작합니다.
        StartCoroutine(waitAndNextTurn(time));
    }


    public void startDeadMonsterSequence(float time, GameObject deadmonster)
    {
        // this.StartCoroutine()을 사용하여 GameManager 오브젝트 위에서 코루틴을 시작합니다.
        StartCoroutine(deadMonsterSequence(time, deadmonster));
    }


}
