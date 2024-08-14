using UnityEngine;
using System.Collections.Generic;

public enum ItemType { Buildable, Weapon, Ammo, Consumeable }

[System.Serializable]
public class ShopItem
{
    public string Name;
    public string Details;
    public GameObject Prefab;
    public Sprite Icon;
    public int Cost;
    public int Quantity = 1;
    public ItemType ItemType;
}

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private List<ShopItem> m_shopItems;
    [SerializeField] private InventoryManager m_inventoryManager;
    [SerializeField] private int m_playerCurrency;

    public void BuyItem(string itemName)
    {
        ShopItem item = m_shopItems.Find(shopItem => shopItem.Name == itemName);
        if (item != null && m_playerCurrency >= item.Cost)
        {
            m_playerCurrency -= item.Cost;

            if (item.ItemType == ItemType.Buildable)
            {
                m_inventoryManager.AddBuildableItem(item.Name, item.Prefab, item.Quantity);
            }
            else if(item.ItemType == ItemType.Ammo)
            {
                m_inventoryManager.AddAmmoItem(item.Name, item.Prefab, item.Quantity);
            }
            else if (item.ItemType == ItemType.Weapon)
            {
                m_inventoryManager.AddWeaponItem(item.Prefab);
            }
            else
            {
                m_inventoryManager.AddConsumeableItem(item.Name, item.Prefab, item.Quantity);
            }

            Debug.Log("Item purchased: " + item.Name);
        }
        else
        {
            Debug.Log("Not enough currency or item not found.");
        }
    }

    public List<ShopItem> GetShopItems()
    {
        return m_shopItems;
    }

    public int GetPlayerCurrency()
    {
        return m_playerCurrency;
    }
}
