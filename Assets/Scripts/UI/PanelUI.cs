#region

using UnityEngine;

#endregion

public class PanelUI : MonoBehaviour
{
    public string menuName;
    public bool open;
    [SerializeField] private GameObject defaultButton;
    public GameObject GetDefaultButton()
    {
        return defaultButton;
    }
    public void SetDefaultButton(GameObject _obj)
    {
        defaultButton = _obj;
    }

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
        AudioManager.Instance.PlaySound("SFX_UIPanelOpen");
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}