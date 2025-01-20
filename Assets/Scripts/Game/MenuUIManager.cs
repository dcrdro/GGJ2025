using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : UIManager<MenuUIManager>
{
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnStartButtonClicked()
    {
        Time.timeScale = 1;
        HideAllUI();
        SceneManager.LoadScene(1);
    }
}