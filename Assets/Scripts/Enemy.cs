using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip hitSound;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            PlaySoundAndDisable();
        }
    }

    void PlaySoundAndDisable()
    {
        if (hitSound != null)
        {
            // Create a temporary audio player at the current position
            GameObject tempAudio = new GameObject("TempHitSound");
            tempAudio.transform.position = transform.position;

            AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
            audioSource.clip = hitSound;
            audioSource.Play();

            Destroy(tempAudio, hitSound.length); // Destroy temp object after sound plays
        }
        else
        {
            Debug.LogWarning("hitSound not assigned on Enemy.");
        }

        gameObject.SetActive(false); // Disable after audio trigger (audio still plays)
    }
}