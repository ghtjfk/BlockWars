using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public SpriteRenderer[] backgrounds;
    public Color32 defaltColor = new Color32(255, 255, 255, 100);

    private void Start()
    {
        int stage = GameManager.Instance.getStage();

        foreach (var bg in backgrounds)
        {
            bg.gameObject.SetActive(false);
        }

        int idx = Mathf.Clamp(stage - 1, 0, backgrounds.Length - 1);
        backgrounds[idx].gameObject.SetActive(true);

        if (stage >= 1 && stage <= backgrounds.Length)
        {
            if(GameManager.Instance.redMoon < 0.1f) // 10% 확률로 붉은 달 이벤트
            {
                setBackgroundColor(idx, new Color32(255, 0, 0, 100));
            }
            else
            {
                setBackgroundColor(idx, defaltColor);
            }
        }
    }

    // 색상 전체 조절 (알파 포함)
    public void setBackgroundColor(int idx, Color32 color)
    {
        if (idx < 0 || idx >= backgrounds.Length) return;
        backgrounds[idx].color = color;
    }


}
