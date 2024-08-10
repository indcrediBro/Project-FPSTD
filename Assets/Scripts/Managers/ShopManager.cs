using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string Name;
        public GameObject Prefab;
        public int Cost;
    }

    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private InventoryManager m_inventoryManager;
    [SerializeField] private int playerCurrency;

    public void BuyItem(string itemName)
    {
        ShopItem item = shopItems.Find(shopItem => shopItem.Name == itemName);
        if (item != null && playerCurrency >= item.Cost)
        {
            playerCurrency -= item.Cost;
            m_inventoryManager.AddItem(item.Name, item.Prefab, 1);
            Debug.Log("Item purchased: " + item.Name);
        }
        else
        {
            Debug.Log("Not enough currency or item not found.");
        }
    }

    public List<ShopItem> GetShopItems()
    {
        return shopItems;
    }

    public int GetPlayerCurrency()
    {
        return playerCurrency;
    }
}
