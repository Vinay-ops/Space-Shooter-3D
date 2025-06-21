using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] AudioClip launchSound;

    private AudioSource audioSource;
    private TargetMarkerController_UI targetMarker;

    void Awake()
    {
        // Find target marker and audio source once
        targetMarker = FindObjectOfType<TargetMarkerController_UI>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Keyboard test only (optional for mobile)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireMissile();
        }
    }

    public void FireMissile()
    {
        if (missilePrefab != null && firePoint != null)
        {
            GameObject missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);

            Transform target = targetMarker?.GetLockedTarget();
            if (target != null)
            {
                missile.GetComponent<HomingMissile>()?.SetTarget(target);
            }

            if (launchSound != null)
            {
                audioSource.PlayOneShot(launchSound);
            }
            else
            {
                Debug.LogWarning("Launch sound not assigned!");
            }
        }
        else
        {
            Debug.LogWarning("Missing missilePrefab or firePoint reference");
        }
    }
}