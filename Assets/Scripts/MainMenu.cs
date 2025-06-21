using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int gameSceneIndex = 1;
    [SerializeField] GameObject settingsPanel;

    [Header("Audio")]
    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        // Play background voice/music on menu load
        if (audioSource != null && backgroundClip != null)
        {
            audioSource.clip = backgroundClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Settings Panel is not assigned!");
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }
}
