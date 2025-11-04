using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    [Header("이 스테이지 번호 (예: 1, 2, 3, 4)")]
    [SerializeField] private int stageNumber = 1;

    [Header("클리어 버튼 (인스펙터에서 드래그)")]
    [SerializeField] private Button clearButton;

    private void Awake()
    {
        if (clearButton == null)
            clearButton = GetComponent<Button>();

        if (clearButton != null)
            clearButton.onClick.AddListener(OnClickClear);
        else
            Debug.LogWarning("ClearStage: clearButton이 연결되어 있지 않습니다.");
    }

    private void OnDestroy()
    {
        if (clearButton != null)
            clearButton.onClick.RemoveListener(OnClickClear);
    }

    // 클리어 버튼 클릭 시 실행: 다음 스테이지 해금 + MapScene으로 이동
    private void OnClickClear()
    {
        Debug.Log($"스테이지 {stageNumber} 클리어! 다음 스테이지 해금 중...");

        // 현재 스테이지를 클리어했으므로 다음 스테이지 해금
        int nextStage = stageNumber + 1;
        MapButtonManager.UnlockUpTo(nextStage);

        SceneManager.LoadScene("MapScene");
    }
}
