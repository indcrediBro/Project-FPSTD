using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private ShopManager m_shopManager;
    [SerializeField] private Transform m_shopPanel;
    [SerializeField] private GameObject m_shopItemPrefab;
    [SerializeField] private TMP_Text m_playerCurrencyText;

    private void Start()
    {
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        foreach (Transform child in m_shopPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItem item in m_shopManager.GetShopItems())
        {
            GameObject shopItemGO = Instantiate(m_shopItemPrefab, m_shopPanel);
            ShopItemUI shopItemUI = shopItemGO.GetComponent<ShopItemUI>();

            shopItemUI.InitializeShopItemUI(item.Name, item.Details, item.Cost.ToString(), item.Icon);
        }
    }
}
