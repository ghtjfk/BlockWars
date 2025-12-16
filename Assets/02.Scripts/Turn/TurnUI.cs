using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUI : MonoBehaviour
{

    public GameObject playerTurnPanel;
    public GameObject monsterSeletTurnPanel;
    public GameObject monsterTurnPanel;

    public IEnumerator showTurnUI()
    {
        switch (TurnManager.Instance.turnState)
        {
            case TurnState.PlayerTurn:
                Time.timeScale = 0f;
                playerTurnPanel.SetActive(true);
                yield return new WaitForSecondsRealtime(1.0f);
                playerTurnPanel.SetActive(false);
                Time.timeScale = 1f;
                break;
            case TurnState.MonsterSelect:
                Time.timeScale = 0f;
                monsterSeletTurnPanel.SetActive(true);
                yield return new WaitForSecondsRealtime(1.0f);
                monsterSeletTurnPanel.SetActive(false);
                Time.timeScale = 1f;
                break;
            case TurnState.MonsterTurn:
                Time.timeScale = 0f;
                monsterTurnPanel.SetActive(true);
                yield return new WaitForSecondsRealtime(1.0f);
                monsterTurnPanel.SetActive(false);
                Time.timeScale = 1f;
                break;
        }
    }
}
