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
    void Start()
    {
        // ⭐ 이 로그가 안 뜨면 스크립트가 비활성화 상태입니다.
        Debug.Log($"[ShopSlot] {gameObject.name} 스크립트 로드 완료."); 
    }

    public void SetItem(ItemData data, ShopUI shopUI)
{
    currentItem = data; 
    parentUI = shopUI;

    // 아이템 이름 할당 확인 (itemNameText가 Text 타입이라고 가정)
    if (itemNameText != null) 
    {
        itemNameText.text = data.itemName;
        Debug.Log($"[ShopSlot] 아이템 이름 할당: {data.itemName}");
    }

    // 아이템 이름 할당 확인 (itemNameText가 Text 타입이라고 가정)
    if (itemNameText != null) 
    {
        // ⭐ data.itemName이 정확하게 할당되었는지 확인
        itemNameText.text = data.itemName;
        Debug.Log($"[ShopSlot] 아이템 이름 할당: {data.itemName}"); // 할당 여부 확인용 로그 추가
    }
    
    // 아이템 가격 할당 확인
    if (itemCostText != null)
    {
        itemCostText.text = data.itemCost.ToString();
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
        Debug.Log($"[ShopSlot] {currentItem?.itemName ?? "빈 슬롯"} 오브젝트 클릭 감지!");
        
        // 실제 구매 로직 실행
        BuyItem();
    }
    public void BuyItem()
    {
        
        if (currentItem == null) 
        {
            Debug.LogError("구매하려는 아이템 데이터(currentItem)가 NULL입니다!");
            return;
        }

        // GameManager 인스턴스 확인
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager 인스턴스를 찾을 수 없습니다! 코인 연동 불가.");
            return;
        }

        // ⭐ 코인 체크: 현재 잔액이 아이템 가격보다 적은지 확인
        int currentCoin = GameManager.Instance.nowPlayer.coin;
        int itemPrice = currentItem.itemCost;

        if (currentCoin < itemPrice)
        {
            Debug.LogWarning($"코인 부족! 현재 코인: {currentCoin}, 필요 코인: {itemPrice}");
            // [TODO] 화면에 "코인 부족" UI 메시지 표시 로직 추가
            return; // 코인이 부족하면 구매를 중단
        }

        // ⭐ 구매 성공: 코인 차감 및 데이터 갱신
        GameManager.Instance.nowPlayer.coin -= itemPrice;
        
        // [TODO] 인벤토리 또는 플레이어 데이터에 아이템 추가 로직
        
        // 코인 차감 후 변경된 데이터 저장 (선택 사항)
        GameManager.Instance.SaveData(); 
        
        // 상점 재고 갱신
        ShopManager.Instance.GenerateNewStock(); 

        // 갱신된 재고를 즉시 화면에 다시 표시하도록 ShopUI에 요청
        parentUI?.DisplayCurrentStock();
        
        Debug.Log($"구매 성공! 아이템: {currentItem.itemName}, 남은 코인: {GameManager.Instance.nowPlayer.coin}");
        // 씬에서 CoinUI 컴포넌트를 찾아서 갱신 함수를 호출합니다.
        CoinUI coinUI = FindObjectOfType<CoinUI>(); 
        if (coinUI != null)
        {
            coinUI.UpdateCoinDisplay();
        }
    }
    
}
