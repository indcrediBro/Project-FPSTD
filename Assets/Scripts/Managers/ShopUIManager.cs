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
        foreach (Transform child in m_shopPanel)
        {
            Destroy(child.gameObject);
        }

        List<ShopItem> list = m_shopManager.GetShopItems();
        GameObject shopItemGOFirst = null;

        for (int i = 0; i < list.Count; i++)
        {
            ShopItem item = list[i];

            GameObject shopItemGO = Instantiate(m_shopItemPrefab, m_shopPanel);
            ShopItemUI shopItemUI = shopItemGO.GetComponent<ShopItemUI>();
            shopItemUI.InitializeShopItemUI(item.Name, item.Details, item.Cost.ToString(), item.Icon);
            if (i == 0) shopItemGOFirst = shopItemGO;
        }
        if (shopItemGOFirst != null)
        {
            MenuManager.Instance.GetPanelUI("ShopPanel").SetDefaultButton(shopItemGOFirst);
        }
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
