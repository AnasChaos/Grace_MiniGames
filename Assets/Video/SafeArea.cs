using UnityEngine;

public class SafeArea : MonoBehaviour
{
    RectTransform safeArea;

    void Start()
    {
        safeArea = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        Rect safeAreaRect = Screen.safeArea;

        Vector2 anchorMin = safeAreaRect.position;
        Vector2 anchorMax = safeAreaRect.position + safeAreaRect.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        safeArea.anchorMin = anchorMin;
        safeArea.anchorMax = anchorMax;
    }
}