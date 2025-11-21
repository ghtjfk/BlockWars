using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour
{
    [Header("UI 요소 연결")]
    public Text itemNameText;
    public Text itemCostText;
    public Image itemIconImage;

    private ItemData currentItem;
    private ShopUI parentUI; 

    public void SetItem(ItemData data, ShopUI shopUI)
{
    // ... (currentItem, parentUI 할당 로직 생략) ...

    // 1. 아이템 이름 할당 확인 (itemNameText가 Text 타입이라고 가정)
    if (itemNameText != null) 
    {
        // ⭐ data.itemName이 정확하게 할당되었는지 확인
        itemNameText.text = data.itemName;
        Debug.Log($"[ShopSlot] 아이템 이름 할당: {data.itemName}"); // 할당 여부 확인용 로그 추가
    }
    
    // 2. 아이템 가격 할당 확인
    if (itemCostText != null)
    {
        itemCostText.text = data.itemCost.ToString();
    }
    
    // 3. 아이템 이미지 할당 확인 (Image 컴포넌트가 연결되어 있다고 가정)
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
        // 마우스 오른쪽 클릭 등 특정 조건이 필요하면 eventData를 사용합니다.
        
        // 실제 구매 로직 실행
        BuyItem();
    }

    /// <summary>
    /// 아이템 구매 및 재고 갱신 로직을 수행합니다.
    /// </summary>
    public void BuyItem()
    {
        if (currentItem == null) return;

        // **[TODO] 실제 구매 로직 (골드 체크, 인벤토리 추가 등) 구현 위치**
        
        // 구매 성공했다고 가정:
        
        // 1. 특정 행동(아이템 구매) 완료 후 상점 재고 갱신 요청
        ShopManager.Instance.GenerateNewStock(); 

        // 2. 갱신된 재고를 즉시 화면에 다시 표시하도록 ShopUI에 요청
        parentUI?.DisplayCurrentStock();
        
        Debug.Log(currentItem.itemName + " 구매 완료. 재고 갱신.");
    }
}
