using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // List.OrderBy().Take() 등을 사용하기 위해 추가

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemCost;
    public Sprite itemIcon; // UI 표시를 위해 필요하다면 추가
}

// 1-2. 상점 데이터 관리자 스크립트
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    
    [Header("전체 아이템 목록")]
    public List<ItemData> allItems; 
    
    [Header("현재 상점 재고 (고정)")]
    // 인스펙터에 숨기고 코드로만 접근하도록 설정
    [HideInInspector]
    public List<ItemData> currentStock = new List<ItemData>(); 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // ⭐ 씬이 바뀌어도 오브젝트를 파괴하지 않고 유지
            DontDestroyOnLoad(gameObject); 
            
            // 최초 로드 시 목록이 비어있다면 아이템 생성
            if (currentStock.Count == 0)
            {
                GenerateNewStock();
            }
        }
        else
        {
            // ⭐ 이미 인스턴스가 존재하면 새로 생성된 오브젝트를 파괴하여 중복 방지
            Destroy(gameObject); 
        }
    }
    
    
    public void GenerateNewStock()
    {
        currentStock.Clear();

        if (allItems == null || allItems.Count == 0) return;

        // 전체 목록에서 무작위로 3개 선택 (중복 방지)
        currentStock = allItems
            .OrderBy(item => Random.value) 
            .Take(3)                      
            .ToList();
        string selectedItems = string.Join(", ", currentStock.Select(i => i.itemName));
        Debug.Log($"[ShopManager] 상점 재고가 새로 갱신되었습니다. 선택된 아이템 (3개): {selectedItems}");
    }
}