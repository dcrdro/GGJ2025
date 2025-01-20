using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public bool IsPaused { get; private set; }

    void Start()
    {
    }

    void Update()
    {
    }

    public void PauseGame()
    {
        if (IsPaused) return;

        IsPaused = true;
        Time.timeScale = 0;
        GameUIManager.Instance.ShowUI(UIPanel.PauseMenu);
    }

    public void UnpauseGame()
    {
        if (!IsPaused) return;

        IsPaused = false;
        Time.timeScale = 1;
        GameUIManager.Instance.HideUI(UIPanel.PauseMenu);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        GameUIManager.Instance.HideAllUI();
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        GameUIManager.Instance.HideAllUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
