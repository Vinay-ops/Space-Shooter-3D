using UnityEngine;
using UnityEngine.UI;

public class TargetIndicatorUI : MonoBehaviour
{
    [Header("UI Reference")]
    public Image targetImage;

    [Header("Colors")]
    public Color normalColor = Color.red;
    public Color lockedColor = Color.green;
    public float colorChangeSpeed = 5f;

    [Header("Auto-Aim")]
    public Transform spaceship; // Assign in Inspector
    public float aimAssistStrength = 2f;

    [Header("Tracking Settings")]
    public Camera mainCamera; // Assign or auto-detect

    private RectTransform rectTransform;
    private bool isLocked = false;
    private Transform lockedTarget;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. Color transition
        if (targetImage != null)
        {
            Color targetColor = isLocked ? lockedColor : normalColor;
            targetImage.color = Color.Lerp(targetImage.color, targetColor, Time.deltaTime * colorChangeSpeed);
        }

        // 2. Auto-Aim spaceship
        if (isLocked && lockedTarget != null && spaceship != null)
        {
            Vector3 direction = (lockedTarget.position - spaceship.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            spaceship.rotation = Quaternion.Slerp(spaceship.rotation, targetRotation, aimAssistStrength * Time.deltaTime);
        }

        // 3. Move UI to track enemy
        if (isLocked && lockedTarget != null && rectTransform != null && mainCamera != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(lockedTarget.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                screenPos,
                mainCamera,
                out Vector2 localPos
            );
            rectTransform.anchoredPosition = localPos;
        }
    }

    public void SetLockStatus(bool locked)
    {
        isLocked = locked;
    }

    public void SetLockedTarget(Transform target)
    {
        lockedTarget = target;
    }
}
