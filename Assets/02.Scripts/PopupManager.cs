using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject popupRoot;      // PopupRoot (활/비활)
    [SerializeField] private CanvasGroup popupCanvas;   // PopupRoot에 달린 CanvasGroup
    [SerializeField] private Image dimmer;              // Dimmer (전체화면 검정 Image)
    [SerializeField] private Text messageText;          // "스테이지 n에 입장하시겠습니까?"

    [Header("Buttons")]
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;

    [Header("Animation")]
    [SerializeField] private float fadeDuration = 0.25f; // 팝업/디머 페이드 시간
    [SerializeField] private float dimmerTargetAlpha = 0.55f;

    private int pendingStage = -1;
    private bool isAnimating = false;

    private void Awake()
    {
        // 초깃값: 비활성 / 투명
        if (popupRoot != null) popupRoot.SetActive(false);
        if (popupCanvas != null)
        {
            popupCanvas.alpha = 0f;
            popupCanvas.interactable = false;
            popupCanvas.blocksRaycasts = false;
        }
        if (dimmer != null)
        {
            var c = dimmer.color;
            dimmer.color = new Color(c.r, c.g, c.b, 0f);
        }

        if (buttonYes != null) buttonYes.onClick.AddListener(OnClickYes);
        if (buttonNo != null)  buttonNo.onClick.AddListener(OnClickNo);
    }

    /// <summary>
    /// 스테이지 번호를 받아 팝업을 연다. (메시지 세팅 + 페이드)
    /// </summary>
    public void Open(int stageNumber)
    {
        if (isAnimating) return;
        pendingStage = stageNumber;

        if (messageText != null)
            messageText.text = $"스테이지 {stageNumber}에 입장하시겠습니까?";

        if (popupRoot != null && !popupRoot.activeSelf)
            popupRoot.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(CoFade(true));
    }

    /// <summary>
    /// 팝업 닫기(페이드 아웃)
    /// </summary>
    public void Close()
    {
        if (isAnimating) return;
        StopAllCoroutines();
        StartCoroutine(CoFade(false));
    }

    private IEnumerator CoFade(bool fadeIn)
    {
        isAnimating = true;

        float t = 0f;
        float fromAlpha = popupCanvas != null ? popupCanvas.alpha : 0f;
        float toAlpha = fadeIn ? 1f : 0f;

        float fromDim = dimmer != null ? dimmer.color.a : 0f;
        float toDim = fadeIn ? dimmerTargetAlpha : 0f;

        // 인터랙션 토글
        if (popupCanvas != null && fadeIn)
        {
            popupCanvas.interactable = false;  // 애니메이션 중엔 막기
            popupCanvas.blocksRaycasts = false;
        }

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime; // 게임 일시정지 대비
            float k = Mathf.Clamp01(t / fadeDuration);

            // CanvasGroup alpha
            if (popupCanvas != null)
                popupCanvas.alpha = Mathf.Lerp(fromAlpha, toAlpha, k);

            // Dimmer alpha
            if (dimmer != null)
            {
                var c = dimmer.color;
                dimmer.color = new Color(c.r, c.g, c.b, Mathf.Lerp(fromDim, toDim, k));
            }

            yield return null;
        }

        // 최종 값 스냅
        if (popupCanvas != null)
        {
            popupCanvas.alpha = toAlpha;
            popupCanvas.interactable = fadeIn;
            popupCanvas.blocksRaycasts = fadeIn;
        }
        if (dimmer != null)
        {
            var c = dimmer.color;
            dimmer.color = new Color(c.r, c.g, c.b, toDim);
        }

        if (!fadeIn && popupRoot != null)
            popupRoot.SetActive(false);

        isAnimating = false;
    }

    private void OnClickNo()
    {
        // "아니오": 팝업 해제(원래 화면으로)
        Close();
    }

    private void OnClickYes()
    {
        // "예": 팝업 해제 + 해당 스테이지로 씬 전환
        int target = pendingStage;
        Close();

        // 전환은 닫기 페이드가 끝난 직후가 자연스럽지만
        // 간단히 즉시 전환하거나, 약간 딜레이를 줄 수 있어요.
        StartCoroutine(CoLoadStageAfterFade(target));
    }

    private IEnumerator CoLoadStageAfterFade(int stage)
    {
        // 닫기 페이드 시간을 고려하여 살짝 대기(시각적으로 깔끔)
        yield return new WaitForSecondsRealtime(fadeDuration * 0.9f);

        string sceneName = $"Stage{stage}";
        // 필요 시 로딩 씬/Transition을 사이에 낄 수 있음
        SceneManager.LoadScene(sceneName);
    }
}
