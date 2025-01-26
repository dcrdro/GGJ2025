using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : UIManager<GameUIManager>
{
    public bool IsPaused { get; private set; }

    public void OnPauseButtonClicked()
    {
        if (IsPaused) return;
        IsPaused = true;
        Time.timeScale = 0;
        ShowUI(UIPanel.PauseMenu);
    }

    public void OnUnpauseButtonClicked()
    {
        if (!IsPaused) return;

        IsPaused = false;
        Time.timeScale = 1;
        HideUI(UIPanel.PauseMenu);
    }

    public void OnMainMenuButtonClicked()
    {
        //GameManager.Instance.GoToMainMenu();
    }

    public void OnRestartButtonClicked()
    {
        //GameManager.Instance.RestartLevel();
    }
}