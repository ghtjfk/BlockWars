using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    private int stageNumber;
    public GameObject gameClearPanel;

    [Header("클리어 버튼 (인스펙터에서 드래그)")]
    [SerializeField] private Button clearButton;

    private void Awake()
    {
        stageNumber = GameManager.Instance.nowPlayer.stage;

        if (clearButton != null) clearButton.onClick.AddListener(OnClickClear);
    }


    public void SetGameClear()
    {
        GameManager.Instance.isClear = true;
        gameClearPanel.SetActive(true);
        SceneManager.UnloadSceneAsync("UIScene");
        Time.timeScale = 0f;

    }
    // 클리어 버튼 클릭 시 실행: 다음 스테이지 해금 + MapScene으로 이동
    public void OnClickClear()
    {
        Debug.Log($"스테이지 {stageNumber} 클리어! 다음 스테이지 해금 중...");
        // 현재 스테이지를 클리어했으므로 다음 스테이지 해금
        int nextStage = stageNumber + 1;
        MapButtonManager.UnlockUpTo(nextStage);
        GameManager.Instance.nowPlayer.stage = nextStage;
        GameManager.Instance.SaveData();
        GameManager.Instance.isClear = false;
        switch (nextStage)
        {
            case 11:
                SceneManager.LoadScene("EndingScene");
                break;
            default:
                SceneManager.LoadScene("MapScene");
                break;
        }
    }
}