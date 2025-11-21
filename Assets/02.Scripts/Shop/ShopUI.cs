using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("아이템 슬롯 3개 연결")]
    public ShopSlot[] shopSlots; 

    public void DisplayCurrentStock()
{
    if (ShopManager.Instance == null) return;
    
    List<ItemData> stock = ShopManager.Instance.currentStock;

    for (int i = 0; i < shopSlots.Length; i++)
    {
        if (i < stock.Count)
        {
            // ⭐ shopSlots[i]가 null이 아닌지, 그리고 ShopSlot 컴포넌트인지 확인
            shopSlots[i].SetItem(stock[i], this); 
            shopSlots[i].gameObject.SetActive(true); // ⭐ 오브젝트 활성화 확인
        }
        else
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }
}
}
