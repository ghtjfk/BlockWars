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
            Debug.Log("�����̽��� �Է�! HP�� 10���� ���� -> " + GameManager.Instance.hp);
        }
    }*/

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    //���콺�� ��ư ���� �÷��� �� �ڵ� ȣ���.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Color c = buttonImage.color;
        c.a = 0.4f; // ���İ��� 0.4�� (�ణ ������)
        buttonImage.color = c;
    }

    //���콺�� ��ư ���� ����� �ڵ� ȣ���.
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = originalColor; // ���� ���� ����
    }
}
