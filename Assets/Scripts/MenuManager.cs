#region

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#endregion

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private PanelUI[] m_panels;
    [SerializeField] private GameObject m_hud;
    [SerializeField] private TMPro.TMP_Text m_deathText;
    [SerializeField] private ScreenFlash m_screenFlash;

    public PanelUI GetPanelUI(string _panelName)
    {
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panels[i].menuName == _panelName)
            {
                return m_panels[i];
            }
        }

        return null;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panels[i].open)
            {
                CloseMenu(m_panels[i]);
            }

            if (m_panels[i].menuName == menuName)
            {
                SetActiveObject(m_panels[i].GetDefaultButton());
                m_panels[i].Open();
            }
        }
    }

    public void OpenMenu(PanelUI menu)
    {
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panels[i].open)
            {
                SetActiveObject(menu.GetDefaultButton());
                CloseMenu(m_panels[i]);
            }
        }

        menu.Open();
        SetActiveObject(menu.GetDefaultButton());
    }

    public void CloseMenu(string menuName)
    {
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panels[i].menuName == menuName)
            {
                m_panels[i].SetDefaultButton(EventSystem.current.currentSelectedGameObject);
                m_panels[i].Close();
            }
        }
    }

    public void CloseMenu(PanelUI menu)
    {
        menu.SetDefaultButton(EventSystem.current.currentSelectedGameObject);
        menu.Close();
    }

    public void ShowHUD()
    {
        foreach (PanelUI menu in m_panels)
        {
            //menu.defaultButton = EventSystem.current.currentSelectedGameObject;
            menu.Close();
        }

        m_hud.SetActive(true);
        //GameStateManager.Instance.SetState(new PlayState(GameStateManager.Instance));
    }

    public void HideHUD()
    {
        m_hud.SetActive(false);
    }

    public void MainMenu()
    {
        GameStateManager.Instance.SetState(new StartState(GameStateManager.Instance));
        LoadScene("Main Menu");
    }

    public void ResumeGame()
    {
        GameStateManager.Instance.SetState(new PlayState(GameStateManager.Instance));
    }

    public void HandleSettingsBackButton()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            OpenMenu("MainMenu");
        }
        else
        {
            OpenMenu("PauseMenu");
        }
    }

    public void SetGameOverText(string _text)
    {
        m_deathText.text = _text;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string _levelToLoad)
    {
        StartCoroutine(LoadSceneCO(_levelToLoad));
    }

    public void SetActiveObject(GameObject obj)
    {
        if (Input.GetJoystickNames().Length != 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(obj);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private IEnumerator LoadSceneCO(string _levelToLoad)
    {
        Time.timeScale = 1;
        m_screenFlash.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_levelToLoad);
        GameStateManager.Instance.SetState(new PlayState(GameStateManager.Instance));
        m_screenFlash.FadeIn();
    }
}