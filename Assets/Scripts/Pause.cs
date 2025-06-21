using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private int mainMenuSceneIndex = 0; // Set this to your main menu scene index

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure time scale is reset before changing scenes
        IsPaused = false;
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
