using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    // 코인을 표시할 Text 컴포넌트 (인스펙터에서 연결)
    private Text coinText;

    private void Awake()
    {
        // 오브젝트에 붙어있는 Text 컴포넌트를 가져옵니다.
        coinText = GetComponent<Text>();
        if (coinText == null)
        {
            Debug.LogError("CoinUI 스크립트는 Text 컴포넌트에 붙어 있어야 합니다.");
        }
    }

    private void Start()
    {
        // 씬 로드 시 최초 1회 코인 표시를 업데이트합니다.
        UpdateCoinDisplay();
    }

    // 이 함수가 외부에서 호출될 때마다 코인을 갱신합니다.
    public void UpdateCoinDisplay()
    {
        if (coinText == null || GameManager.Instance == null) return;

        // GameManager에서 현재 코인 값을 가져와 Text에 할당
        int currentCoin = GameManager.Instance.nowPlayer.coin;
        coinText.text = currentCoin.ToString();
        
        Debug.Log($"[CoinUI] 코인 텍스트 갱신 완료: {currentCoin}");
    }
}