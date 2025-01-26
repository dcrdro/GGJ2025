using System;
using System.Collections.Generic;
using UnityEngine;


public class GameUIManager : UIManager<GameUIManager>
{
    public GameObject inventory;
    public bool IsPaused { get; private set; }

    public void OnPauseButtonClicked()
    {
        if (IsPaused) return;
        IsPaused = true;
        inventory.SetActive(false);
        Time.timeScale = 0;
        ShowUI(UIPanel.PauseMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                OnUnpauseButtonClicked();
            }
            else
            {
                OnPauseButtonClicked();
            }
        }
    }

    public void OnUnpauseButtonClicked()
    {
        if (!IsPaused) return;

        IsPaused = false;
        inventory.SetActive(true);
        Time.timeScale = 1;
        HideUI(UIPanel.PauseMenu);
    }
    
    public void OnExitGameButtonClicked()
    {
        Time.timeScale = 1;
        Application.Quit();
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