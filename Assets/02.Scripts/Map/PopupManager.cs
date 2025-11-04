using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PopupManager : MonoBehaviour
{
    
    [Header("Refs")]
    [SerializeField] private GameObject popupRoot;      // PopupRoot (활/비활)
    [SerializeField] private CanvasGroup popupCanvas;   // PopupRoot에 달린 CanvasGroup
    [SerializeField] private Image dimmer;              // Dimmer (전체화면 검정 Image)
    [SerializeField] private TMP_Text messageText;      // "Do you want to enter the Stage n?"
    
    [Header("Buttons")]
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;

    [Header("Animation")]
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private float dimmerTargetAlpha = 0.55f;

    private int pendingStage = -1;
    private bool isAnimating = false;

    private void Awake()
    {
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

    // 팝업 열기
    public void Open(int stageNumber)
    {
        if (isAnimating) return;
        pendingStage = stageNumber;

        if (messageText != null)
            messageText.text = $"Do you want to enter the Stage {stageNumber}?";

        if (popupRoot != null && !popupRoot.activeSelf)
            popupRoot.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(CoFade(true));
    }

    // 팝업 닫기
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

        if (popupCanvas != null && fadeIn)
        {
            popupCanvas.interactable = false;  // 애니메이션 중엔 막기
            popupCanvas.blocksRaycasts = false;
        }

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime; // 게임 일시정지 대비
            float k = Mathf.Clamp01(t / fadeDuration);

            if (popupCanvas != null)
                popupCanvas.alpha = Mathf.Lerp(fromAlpha, toAlpha, k);

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

    private void OnClickNo()        // "아니오" : 팝업 해제(원래 화면으로)
    {
        Close();
    }

    private void OnClickYes()       // "예" : 팝업 해제 + 해당 스테이지로 씬 전환
    {
        int target = pendingStage;
        Close();
        StartCoroutine(CoLoadStageAfterFade(target));
    }

    private IEnumerator CoLoadStageAfterFade(int stage)
    {
        yield return new WaitForSecondsRealtime(fadeDuration * 0.9f);

        string sceneName = $"Stage{stage}";
        SceneManager.LoadScene(sceneName);
    }
}
