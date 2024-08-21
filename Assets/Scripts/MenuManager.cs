<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
=======
#region

>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance;

    [SerializeField] Menu[] menus;
=======
#endregion

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private Menu[] menus;
>>>>>>> Stashed changes

    void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        for (var i = 0; i < menus.Length; i++)
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
        for (var i = 0; i < menus.Length; i++)
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
        SceneManager.LoadScene("Gary Scene");
    }
}