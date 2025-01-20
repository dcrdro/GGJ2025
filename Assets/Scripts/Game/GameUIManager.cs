using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : UIManager<GameUIManager>
{
    public void OnPauseButtonClicked()
    {
        GameManager.Instance.PauseGame();
    }

    public void OnUnpauseButtonClicked()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void OnMainMenuButtonClicked()
    {
        GameManager.Instance.GoToMainMenu();
    }

    public void OnRestartButtonClicked()
    {
        GameManager.Instance.RestartLevel();
    }
}