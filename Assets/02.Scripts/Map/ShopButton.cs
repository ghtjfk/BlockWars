using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ShopButton : MonoBehaviour
{
    // 인스펙터에서 MapScene에 있는 비활성화된 'Shop' 오브젝트를 연결
    public GameObject shopObject; 
    
    // ShopObject에 붙어있는 ShopUI 컴포넌트 참조
    private ShopUI shopUI; 

    void Start()
    {
        if (shopObject != null)
        {
            // ShopObject에서 ShopUI 컴포넌트를 미리 가져옵니다.
            shopUI = shopObject.GetComponent<ShopUI>();
            if (shopUI == null)
            {
                Debug.LogError("Shop Object에 ShopUI.cs 스크립트가 없습니다! 연결을 확인하세요.");
            }
        }
    }

    // 이 함수는 'ShopButton' 오브젝트의 Button 컴포넌트 OnClick 이벤트에 연결되어야 합니다.
    public void ToggleShopVisibility()
    {
        if (shopObject != null)
        {
            // 상점의 현재 활성화 상태를 반전시킵니다.
            bool willBeActive = !shopObject.activeSelf;
            shopObject.SetActive(willBeActive);

            if (willBeActive && shopUI != null)
            {
                // 상점 패널이 활성화될 때 (열릴 때), ShopManager의 고정된 아이템 목록을 
                // 화면에 표시하도록 ShopUI에게 명령합니다.
                shopUI.DisplayCurrentStock(); 
                Debug.Log("[ShopButton] 상점 UI 활성화 및 목록 표시 요청 완료.");
            }
        }
    }
    
}