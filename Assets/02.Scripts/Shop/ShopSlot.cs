using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopSlot : MonoBehaviour , IPointerClickHandler
{
    [Header("UI 요소 연결")]
    public TMP_Text itemNameText;
    public TMP_Text itemCostText;
    public Image itemIconImage;

    private ItemData currentItem;
    private ShopUI parentUI; 

    public void SetItem(ItemData data, ShopUI shopUI)
{
    currentItem = data; 
    parentUI = shopUI;

    // 아이템 이름 할당 확인 (itemNameText가 Text 타입이라고 가정)
    if (itemNameText != null) 
    {
        itemNameText.text = data.itemName;
    }

    // 아이템 이름 할당 확인 (itemNameText가 Text 타입이라고 가정)
    if (itemNameText != null) 
    {
        // ⭐ data.itemName이 정확하게 할당되었는지 확인
        itemNameText.text = data.itemName;
    }
    
    // 아이템 가격 할당 확인
    if (itemCostText != null)
    {
        itemCostText.text = data.itemCost.ToString()+" $";
    }
    
    // 아이템 이미지 할당 확인 (Image 컴포넌트가 연결되어 있다고 가정)
    if (itemIconImage != null)
    {
        // ⭐ data.itemIcon이 null이 아닌지 확인하고 할당
        itemIconImage.sprite = data.itemIcon;
        itemIconImage.gameObject.SetActive(data.itemIcon != null);
    }
}
    
    // ⭐ 구매 버튼 대신, 오브젝트를 클릭했을 때 호출되는 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        
        // 실제 구매 로직 실행
        BuyItem();
    }
    public void BuyItem()
    {
        if (currentItem == null) 
        {
            return;
        }

        if (GameManager.Instance == null)
        {
            return;
        }

        int currentCoin = GameManager.Instance.nowPlayer.coin;
        int itemPrice = currentItem.itemCost;

        // 1. 코인 체크
        if (currentCoin < itemPrice)
        {
            return; 
        }

        // 2. ⭐ 구매 성공: 코인 차감
        GameManager.Instance.nowPlayer.coin -= itemPrice;
        
        // 3. ⭐ 아이템 효과 즉시 적용 (가장 중요한 부분)
        // GameManager에서 HP/DMG 스탯을 증가시키고 SaveData()도 호출됨.
        GameManager.Instance.ApplyShopEffect(currentItem.itemName); 
        
        StatusUI statusUI = FindObjectOfType<StatusUI>(); 
        if (statusUI != null)
        {
            statusUI.UpdateAllDisplays(); // 코인, HP, 공격력 모두 갱신
        }
        
        // 5. 상점 재고 갱신 (고정 목록이므로 ShopManager의 GenerateNewStock/GenerateFixedStock 호출)
        // Note: 이름은 ShopManager의 실제 함수명에 맞춰주세요. (GenerateNewStock으로 가정)
        ShopManager.Instance.GenerateNewStock(); 

        // 6. 갱신된 재고를 즉시 화면에 다시 표시
        parentUI?.DisplayCurrentStock();
        
        Debug.Log($"구매 성공! 아이템: {currentItem.itemName}, 남은 코인: {GameManager.Instance.nowPlayer.coin}");
    }
}
