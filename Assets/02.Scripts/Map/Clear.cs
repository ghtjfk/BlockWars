using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    private int stageNumber;
    public GameObject gameClearPanel;
    private void Awake()
    {
        stageNumber = GameManager.Instance.nowPlayer.stage;
    }

    //[Header("클리어 버튼 (인스펙터에서 드래그)")]
    //[SerializeField] private Button clearButton;


    public void SetGameClear()
    {
        gameClearPanel.SetActive(true);
        SceneManager.UnloadSceneAsync("UIScene");
        Time.timeScale = 0f;

    }
    // 클리어 버튼 클릭 시 실행: 다음 스테이지 해금 + MapScene으로 이동
    public void OnClickClear()
    {
        Debug.Log($"스테이지 {stageNumber} 클리어! 다음 스테이지 해금 중...");
        if (ShopManager.Instance != null)
        {
            // 특정 행동(스테이지 클리어)으로 간주하여 상점 재고를 새로 랜덤으로 돌립니다.
            ShopManager.Instance.GenerateNewStock();
            Debug.Log("스테이지 클리어로 인해 상점 재고가 갱신되었습니다.");
        }
        else
        {
            Debug.LogError("ShopManager 인스턴스를 찾을 수 없습니다. 씬에 배치되었는지 확인하세요.");
        }
        // 현재 스테이지를 클리어했으므로 다음 스테이지 해금
        int nextStage = stageNumber + 1;
        MapButtonManager.UnlockUpTo(nextStage);
        GameManager.Instance.nowPlayer.stage = nextStage;
        GameManager.Instance.SaveData();

        SceneManager.LoadScene("MapScene");
    }
}
