using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private PanelUI[] m_panels;
    [SerializeField] private GameObject m_hud;

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panels[i].menuName == menuName)
            {
                m_panels[i].Open();
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(m_panels[i].defaultButton);
            }
            else if (m_panels[i].open)
            {
                CloseMenu(m_panels[i]);
            }
        }
    }

    public void OpenMenu(PanelUI menu)
    {
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panels[i].open)
            {
                CloseMenu(m_panels[i]);
            }
        }
        menu.Open();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menu.defaultButton);
    }

    public void CloseMenu(string menuName)
    {
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panels[i].menuName == menuName)
            {
                m_panels[i].Close();
                m_panels[i].defaultButton = EventSystem.current.currentSelectedGameObject;
            }
        }
    }

    public void CloseMenu(PanelUI menu)
    {
        menu.Close();
        menu.defaultButton = EventSystem.current.currentSelectedGameObject;
    }

    public void ShowHUD()
    {
        if (m_hud.activeInHierarchy) return;

        foreach (PanelUI menu in m_panels)
        {
            menu.Close();
            menu.defaultButton = EventSystem.current.currentSelectedGameObject;
        }
        m_hud.SetActive(true);
        GameStateManager.Instance.SetState(new PlayState(GameStateManager.Instance));
    }

    public void HideHUD()
    {
        m_hud.SetActive(false);
    }

    public void MainMenu()
    {
        GameStateManager.Instance.SetState(new StartState(GameStateManager.Instance));
        LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        GameStateManager.Instance.SetState(new PlayState(GameStateManager.Instance));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string _levelToLoad)
    {
        SceneManager.LoadScene(_levelToLoad);
        GameStateManager.Instance.SetState(new PlayState(GameStateManager.Instance));
    }
}
