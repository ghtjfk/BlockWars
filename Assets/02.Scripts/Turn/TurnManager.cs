using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TurnState
{
    PlayerTurn,
    MonsterSelect,
    MonsterTurn
}

public class TurnManager : Singleton<TurnManager>
{


    public TurnState turnState = TurnState.PlayerTurn;
    public TurnUI turnUI;
    public bool isTurnChanging = false;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        turnUI = FindAnyObjectByType<TurnUI>();
    }

    public void NextTurn()
    {
        switch (turnState)
        {
            case TurnState.PlayerTurn:
                if (ModeSwitcher.Instance.GetCurrentMode() || PlayerManager.Instance.getBreakBrickCount() == 0)
                {
                    turnState = TurnState.MonsterTurn;
                    StartCoroutine(turnUI.showTurnUI());
                    StartCoroutine(MonsterManager.Instance.OnMonsterTurnStart());
                    break;
                }
                turnState = TurnState.MonsterSelect;
                StartCoroutine(turnUI.showTurnUI());
                ModeSwitcher.Instance.DecreaseCooldown();
                break;
            case TurnState.MonsterSelect:
                turnState = TurnState.MonsterTurn;
                //�ڷ�ƾ�� �Ʒ� ������� ȣ���ؾ���
                StartCoroutine(MonsterManager.Instance.OnMonsterTurnStart());
                break;
            case TurnState.MonsterTurn:
                turnState = TurnState.PlayerTurn;
                StartCoroutine(turnUI.showTurnUI());
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
        StartCoroutine(turnUI.showTurnUI());

    }

    public IEnumerator deadMonsterSequence(float time, GameObject deadmonster)
    {

        yield return new WaitForSeconds(time);
        Destroy(deadmonster);
        NextTurn();
        StartCoroutine(turnUI.showTurnUI());



    }

    public void startWait(float time)
    {
        // this.StartCoroutine()�� ����Ͽ� GameManager ������Ʈ ������ �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(wait(time));
    }



    public void startWaitAndNextTurn(float time)
    {
        // this.StartCoroutine()�� ����Ͽ� GameManager ������Ʈ ������ �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(waitAndNextTurn(time));
    }


    public void startDeadMonsterSequence(float time, GameObject deadmonster)
    {
        // this.StartCoroutine()�� ����Ͽ� GameManager ������Ʈ ������ �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(deadMonsterSequence(time, deadmonster));
    }


}
