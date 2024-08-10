using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private ShopManager m_shopManager;
    [SerializeField] private Transform shopPanel;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private TMP_Text playerCurrencyText;

    private void Start()
    {
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        foreach (Transform child in shopPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in m_shopManager.GetShopItems())
        {
            GameObject shopItemGO = Instantiate(shopItemPrefab, shopPanel);
            ShopItemUI shopItemUI = shopItemGO.GetComponent<ShopItemUI>();

            shopItemUI.InitializeShopItemUI(item.Name, item.Details, item.Cost.ToString(), item.Icon);
        }

        playerCurrencyText.text = "Currency: " + m_shopManager.GetPlayerCurrency();
    }
}
