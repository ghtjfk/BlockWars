using UnityEngine;
using UnityEngine.UI;

public class MapButtonManager : MonoBehaviour
{
    [Header("Stage Buttons (1~10)")]
    [SerializeField] private Button[] stageButtons;  // 인스펙터에 10개 순서대로 할당 (버튼)

    [Header("Lock Images (1~10)")]
    [SerializeField] private Image[] stageLocks;    // 인스펙터에 10개 순서대로 할당 (자물쇠 이미지)

    [Header("Managers")]
    [SerializeField] private PopupManager popupManager;
    [SerializeField] private MapManager mapManager;  // 필요시(동일 씬 내 이동용)
    [SerializeField] private Button resetButton;

    [Header("Unlock Settings")]
    [SerializeField] private int minStage = 1;
    [SerializeField] private int maxStage = 10;

    private void Awake()        // 초기 해금 상태 구성: 기본은 1만 해금
    {

        if(GameManager.Instance.isGameOver == true)
        {
            ResetProgress();
            GameManager.Instance.isGameOver = false;
        }
        int unlockedMax = PlayerPrefs.GetInt("UnlockedStageMax", 1);
        unlockedMax = Mathf.Clamp(unlockedMax, minStage, maxStage);

        int unlocked = PlayerPrefs.GetInt("UnlockedStageMax", 1);
        unlocked = Mathf.Clamp(unlocked, minStage, maxStage);


        // 버튼 바인딩
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNumber = i + 1;
            if (stageNumber < minStage || stageNumber > maxStage) continue;

            var btn = stageButtons[i];
            var img = stageLocks[i];
            if (btn == null) continue;

            bool isUnlocked = (stageNumber == unlocked);
            btn.interactable = isUnlocked;  // 스테이지 버튼 해금 (스테이지 번호가 Max보다 작거나 같을 때)
            img.gameObject.SetActive(!isUnlocked); // 자물쇠 이미지 해금

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                // 잠금이면 팝업 띄우지 않음
                if (!IsUnlocked(stageNumber))
                {
                    Debug.Log($"Stage {stageNumber} is locked.");
                    return;
                }

                popupManager.Open(stageNumber);

                // (다음 단계용) 캐릭터가 버튼 위치로 이동 애니메이션 훅
                // mapManager?.MoveCharacterToStage(stageNumber);
            });
        }
    }

    private void Start()
    {
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetProgress);
    }

    // 진행도 초기화: 스테이지 1만 해금
    private void ResetProgress()
    {
        PlayerPrefs.SetInt("UnlockedStageMax", 1);
        PlayerPrefs.Save();

        // 💡 자물쇠 그림 초기화 (인덱스 0 제외하고 모두 활성화)
        // stageLocks.Length는 배열의 실제 크기입니다 (예: 10).
        for (int j = 0; j < stageLocks.Length; j++)
        {
            // 1스테이지 (인덱스 0) 자물쇠는 비활성화(잠금 해제)
            bool isLocked = (j != 0);

            // stageLocks[j]가 null이 아닌지 확인 (인스펙터 할당 오류 대비)
            if (stageLocks[j] != null)
            {
                stageLocks[j].gameObject.SetActive(isLocked);
            }
        }

        // 버튼 상태만 다시 반영 (이것이 Unlock된 상태를 UI에 최종 반영함)
        ApplyUnlockStateToButtons();

        Debug.Log("[MapButtonManager] Progress reset → UnlockedStageMax = 1");
    }

    // 현재 PlayerPrefs의 해금 상태를 버튼들에 반영
    private void ApplyUnlockStateToButtons()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedStageMax", 1);
        unlocked = Mathf.Clamp(unlocked, minStage, maxStage);

        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNumber = i + 1;
            if (stageNumber < minStage || stageNumber > maxStage) continue;

            var btn = stageButtons[i];
            if (btn == null) continue;

            bool isUnlocked = (stageNumber == unlocked);
            btn.interactable = isUnlocked;
        }
    }

    public static bool IsUnlocked(int stage)
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedStageMax", 1);
        return stage == unlocked;
    }

    // 스테이지 클리어 시 다음 스테이지 해금에 사용
    public static void UnlockUpTo(int stage)
    {
        stage = Mathf.Max(stage, 1);
        PlayerPrefs.SetInt("UnlockedStageMax", stage);
        PlayerPrefs.Save();
    }
}
