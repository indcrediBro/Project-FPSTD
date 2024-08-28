using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : Singleton<ShopUIManager>
{
    [SerializeField] private ShopManager m_shopManager;
    [SerializeField] private Transform m_shopPanel;
    [SerializeField] private GameObject m_shopItemPrefab;
    [SerializeField] private TMP_Text m_purchaseText;

    private void Start()
    {
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        List<ShopItem> shopItemList = m_shopManager.GetShopItems();
        List<ShopItemUI> shopItemUIList = new List<ShopItemUI>();
        GameObject shopItemGOFirst = null;

        for (int i = 0; i < m_shopPanel.childCount; i++)
        {
            m_shopPanel.GetChild(i).TryGetComponent(out ShopItemUI itemUI);
            if (itemUI) shopItemUIList.Add(itemUI);
        }

        for (int i = 0; i < shopItemList.Count; i++)
        {
            ShopItem item = shopItemList[i];

            shopItemUIList[i].InitializeShopItemUI(item.Name, item.Details, "Cost: " + item.Cost.ToString(), item.Icon);
            if (i == 0) shopItemGOFirst = shopItemUIList[i].gameObject;
        }
        if (shopItemGOFirst != null)
        {
            MenuManager.Instance.GetPanelUI("ShopPanel").SetDefaultButton(shopItemGOFirst);
        }
    }

    public void MoveShopItemToBottom(Transform _shopItem)
    {
        _shopItem.SetAsLastSibling();
    }

    public void DisplayPurchases(string _details)
    {
        StartCoroutine(DisplayPurchaseDetailsCO(_details));
    }

    private IEnumerator DisplayPurchaseDetailsCO(string _details)
    {
        m_purchaseText.text = "";
        m_purchaseText.text = _details;
        yield return new WaitForSeconds(1f);
        m_purchaseText.text = "";
    }
}
