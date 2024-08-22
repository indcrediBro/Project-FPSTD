using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private Menu[] menus;

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(menus[i].defaultButton);
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menu.defaultButton);
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
        menu.defaultButton = EventSystem.current.currentSelectedGameObject;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Level 1");
        GameStateManager.Instance.SetState(new PlayState(GameStateManager.Instance));
    }
}
