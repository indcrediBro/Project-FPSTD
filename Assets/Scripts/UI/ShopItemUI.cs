#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text m_itemNameText;
    [SerializeField] private TMP_Text m_itemDescriptionText;
    [SerializeField] private TMP_Text m_itemCostText;
    [SerializeField] private Image m_itemIconImage;
    [SerializeField] private Button m_buyButton;

    public void InitializeShopItemUI(string _name, string _description, string _cost, Sprite _icon)
    {
        m_itemNameText.text = _name;
        m_itemDescriptionText.text = _description;
        m_itemCostText.text = _cost;
        m_itemIconImage.sprite = _icon;
        m_buyButton.onClick.AddListener(() => ShopManager.Instance.BuyItem(_name));
    }
}