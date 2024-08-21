#region

using UnityEngine;

#endregion

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;
    public GameObject defaultButton;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}