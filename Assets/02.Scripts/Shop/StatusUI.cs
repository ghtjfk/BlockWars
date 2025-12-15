using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [Header("UI 요소 연결 (기본 Text)")]
    public Text coinText; 
    public Text hpText; 
    public Text damageText; 
    

    private void Start()
    {
        // 씬 로드 시 최초 1회 모든 상태 표시를 업데이트합니다.
        UpdateAllDisplays();
    }

    // 이 함수를 외부(상점, 전투, 데미지 피격)에서 호출하여 모든 상태를 갱신합니다.
    public void UpdateAllDisplays()
    {
        if (GameManager.Instance == null) 
        {
            Debug.LogError("GameManager 인스턴스를 찾을 수 없습니다.");
            return;
        }

        // GameManager 인스턴스를 참조합니다.
        var player = GameManager.Instance.nowPlayer;
        
        // 보너스가 적용된 최종 스탯을 가져옵니다.
        float finalMaxHP = GameManager.Instance.GetPlayerMaxHP();
        float finalAttackDamage = GameManager.Instance.GetPlayerAttackDamage();

        // 1. 코인 갱신
        if (coinText != null)
        {
            // ⭐ nowPlayer.coin 사용
            coinText.text = player.coin.ToString();
        }
        
        // 2. HP 갱신
        if (hpText != null)
        {
            // ⭐ curruntHP와 GetPlayerMaxHP() 사용
            // Mathf.CeilToInt를 사용하여 소수점 아래를 버리고 정수로 표시합니다.
            hpText.text = $"{Mathf.CeilToInt(player.curruntHP)} / {Mathf.CeilToInt(finalMaxHP)}";
        }
        // 3. 공격력 갱신
        if (damageText != null)
        {
            // ⭐ GetPlayerAttackDamage() 사용
            damageText.text = finalAttackDamage.ToString("F1"); // 소수점 1자리까지 표시
        }
        
        Debug.Log($"[StatusUI] 갱신 완료 - HP: {player.curruntHP}/{finalMaxHP}, DMG: {finalAttackDamage}");
    }
}