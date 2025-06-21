using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        Cursor.visible = true; // optional: hide system cursor
    }

    void Update()
    {
        rect.position = Input.mousePosition;
    }
}
