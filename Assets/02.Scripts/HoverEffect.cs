using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Color originalColor;
    
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.hp = 10;
            Debug.Log("스페이스바 입력! HP를 10으로 변경 -> " + GameManager.Instance.hp);
        }
    }*/

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    //마우스를 버튼 위에 올렸을 때 자동 호출됨.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Color c = buttonImage.color;
        c.a = 0.4f; // 알파값을 0.4로 (약간 불투명)
        buttonImage.color = c;
    }

    //마우스를 버튼 위에 벗어나면 자동 호출됨.
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = originalColor; // 원래 알파 복구
    }
}
