#region

using UnityEngine;

#endregion

public class PanelUI : MonoBehaviour
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