using UnityEngine;
using UnityEngine.UI;

public class TargetMarkerController_UI : MonoBehaviour
{
    [Header("Target Detection")]
    public float detectDistance = 100f;
    public LayerMask enemyLayer;

    [Header("UI References")]
    public Image markerImage;
    public Color normalColor = Color.red;
    public Color lockColor = Color.green;

    private Camera cam;
    private RectTransform rectTransform;
    private Transform lockedTarget;

    public Transform GetLockedTarget() => lockedTarget;

    void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();

        if (markerImage != null) markerImage.color = normalColor;
        if (cam == null) Debug.LogError("No MainCamera found.");
    }

    void Update()
    {
        DetectEnemy();
    }

    void DetectEnemy()
    {
        lockedTarget = null;

        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(rectTransform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, detectDistance, enemyLayer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                lockedTarget = hit.collider.transform;
                markerImage.color = lockColor;
                return;
            }
        }

        markerImage.color = normalColor;
    }
}
