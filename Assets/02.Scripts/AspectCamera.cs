using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectCamera : MonoBehaviour
{
    [Header("Target Aspect (Width : Height)")]
    public float targetWidth = 9f;
    public float targetHeight = 16f;

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        ApplyAspect();
    }

    void OnValidate()
    {
        // Inspector에서 값 바꿀 때도 즉시 반영 (에디터 편의)
        if (cam == null)
            cam = GetComponent<Camera>();

        ApplyAspect();
    }

    void ApplyAspect()
    {
        float targetAspect = targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;

        if (windowAspect > targetAspect)
        {
            // 좌우가 남음 (pillarbox)
            float scale = targetAspect / windowAspect;
            cam.rect = new Rect(
                (1f - scale) / 2f,
                0f,
                scale,
                1f
            );
        }
        else
        {
            // 위아래가 남음 (letterbox)
            float scale = windowAspect / targetAspect;
            cam.rect = new Rect(
                0f,
                (1f - scale) / 2f,
                1f,
                scale
            );
        }
    }
}