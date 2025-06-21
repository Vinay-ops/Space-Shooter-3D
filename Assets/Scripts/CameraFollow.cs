using UnityEngine;

public class SpaceshipCameraController : MonoBehaviour
{
    public Transform target; // Assign the spaceship here
    public Vector3 offset = new Vector3(0f, 2f, -10f);
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (!target) return;

        // Smooth position follow
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate to match spaceship orientation
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
