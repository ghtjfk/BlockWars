using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[System.Serializable]
public class ItemData
{
        public string itemName;
        public int itemCost;
        public Sprite itemIcon; 
}

public class ShopManager : MonoBehaviour
{
        public static ShopManager Instance;
        [Header("UI 슬롯 연결")]
        // ⭐ 인스펙터에서 UI 슬롯 오브젝트들을 드래그 앤 드롭으로 연결할 배열
        public ShopSlot[] shopSlots;
        
        [Header("현재 상점 재고 (고정)")]
        [HideInInspector]
        public List<ItemData> currentStock = new List<ItemData>(); 

    // ⭐ 1. 고정 아이템 목록을 인스펙터에서 설정할 배열 추가
    [Header("⭐ 고정 판매 아이템 목록 (HP UP, DMG UP 등)")]
    public ItemData[] fixedShopItems; 
         
        private void Awake()
        {
            if (Instance == null)
         {
             Instance = this;
             DontDestroyOnLoad(gameObject); 
             
             if (currentStock.Count == 0)
             {
                // Awake에서는 아직 UI 컴포넌트가 준비되지 않았을 수 있으므로
                // 데이터를 먼저 설정하고 UI 갱신은 ShopUI에서 맡깁니다.
                 GenerateNewStock(); // ⭐ 함수 이름 변경 및 호출
            }
         }
         else
        {
            Destroy(gameObject); 
        }
    }
     
        // ⭐ 2. 랜덤 로직 대신 고정 배열을 currentStock에 복사하는 함수
    public void GenerateNewStock() 
{
    currentStock.Clear();

    if (fixedShopItems == null || fixedShopItems.Length == 0)
    {
        Debug.LogError("[ShopManager] 고정 아이템 목록(fixedShopItems)이 비어있습니다.");
        return;
    }
    
    // 고정 배열의 모든 아이템을 currentStock에 추가
    currentStock.AddRange(fixedShopItems);

    // ⭐ 1. UI 슬롯과 아이템 데이터 개수가 일치하는지 확인
    if (shopSlots == null || shopSlots.Length == 0 || shopSlots.Length != currentStock.Count)
    {
        Debug.LogError("[ShopManager] 슬롯 UI 연결 상태 또는 개수가 잘못되었습니다. 인스펙터를 확인하세요.");
        return;
    }

    // ⭐ 2. 슬롯에 아이템 데이터 할당 (가장 중요한 연결 부분)
    for (int i = 0; i < currentStock.Count; i++)
    {
        
        ShopUI shopUI = FindObjectOfType<ShopUI>(); 
        
        if (shopSlots[i] != null)
        {
            shopSlots[i].SetItem(currentStock[i], shopUI); 
        }
    }

    Debug.Log($"[ShopManager] 상점 UI 슬롯에 고정 목록 할당 완료.");
}
    
}