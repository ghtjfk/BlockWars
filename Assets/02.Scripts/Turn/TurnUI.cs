using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnUI : MonoBehaviour
{

    public GameObject playerTurnPanel;
    public GameObject monsterSeletTurnPanel;
    public GameObject monsterTurnPanel;



    public void ShowPlayerTurnPanel(float duration = 1f)
    {
        StopAllCoroutines(); // 중복 호출 방지
        StartCoroutine(PlayerTurnRoutine(duration));
    }

    private IEnumerator PlayerTurnRoutine(float duration)
    {
        Time.timeScale = 0f;
        playerTurnPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(duration);
        playerTurnPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public IEnumerator showTurnUI()
    {
        if(GameManager.Instance.isClear)
        {
            yield break;
        }

        TurnManager.Instance.isTurnChanging = true;
        //SceneManager.UnloadSceneAsync("UIScene");

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

        TurnManager.Instance.isTurnChanging = false;
        //SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
    }
}
