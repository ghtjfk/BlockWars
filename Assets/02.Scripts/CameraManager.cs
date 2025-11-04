using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [Header("Follow")]
    public Transform target;            // 따라갈 대상(캐릭터)
    public float followLerp = 4f;       // 따라가는 부드러움

    [Header("Zoom")]
    public float defaultOrthoSize = 6f; // 초기 카메라 크기 (9:16 게임뷰 맞춰 사용)
    public float zoomLerp = 2f;         // 줌 속도(보간)
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic) cam.orthographicSize = defaultOrthoSize;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 맵이 세로 스크롤 구조라면 X,Z 고정하고 Y만 따라가면 안정적
        Vector3 desired = new Vector3(transform.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desired, followLerp * Time.deltaTime);
    }

    public void SetTarget(Transform t) => target = t;

    public void SetDefaultSize(float size)
    {
        defaultOrthoSize = size;
        if (cam.orthographic) cam.orthographicSize = defaultOrthoSize;
    }

    public void ZoomIn(float focusSize)  { StopAllCoroutines(); StartCoroutine(ZoomTo(focusSize)); }
    public void ZoomOut()                { StopAllCoroutines(); StartCoroutine(ZoomTo(defaultOrthoSize)); }

    public IEnumerator ZoomTo(float newSize)
    {
        if (!cam.orthographic) yield break;
        float start = cam.orthographicSize;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * zoomLerp;
            cam.orthographicSize = Mathf.Lerp(start, newSize, t);
            yield return null;
        }
        cam.orthographicSize = newSize;
    }
}
