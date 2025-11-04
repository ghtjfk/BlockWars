using UnityEngine;
using UnityEngine.UI;

public class MapButtonManager : MonoBehaviour
{
    [Header("Stage Buttons (1~10)")]
    [SerializeField] private Button[] stageButtons;  // 인스펙터에 10개 순서대로 할당

    [Header("Managers")]
    [SerializeField] private PopupManager popupManager;
    [SerializeField] private MapManager mapManager; // 필요시(동일 씬 내 이동용)

    [Header("Unlock Settings")]
    [SerializeField] private int minStage = 1;
    [SerializeField] private int maxStage = 10;

    private void Awake()        // 초기 해금 상태 구성: 기본은 1만 해금
    {
        int unlockedMax = PlayerPrefs.GetInt("UnlockedStageMax", 1);
        unlockedMax = Mathf.Clamp(unlockedMax, minStage, maxStage);

        // 버튼 바인딩
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNumber = i + 1;
            if (stageNumber < minStage || stageNumber > maxStage) continue;

            var btn = stageButtons[i];
            if (btn == null) continue;

            bool isUnlocked = (stageNumber <= unlockedMax);
            btn.interactable = isUnlocked;

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

    public static bool IsUnlocked(int stage)
    {
        int unlockedMax = PlayerPrefs.GetInt("UnlockedStageMax", 1);
        return stage <= unlockedMax;
    }

    // 스테이지 클리어 시 다음 스테이지 해금에 사용
    public static void UnlockUpTo(int stage)
    {
        int prev = PlayerPrefs.GetInt("UnlockedStageMax", 1);
        if (stage > prev)
        {
            PlayerPrefs.SetInt("UnlockedStageMax", stage);
            PlayerPrefs.Save();
        }
    }
}
