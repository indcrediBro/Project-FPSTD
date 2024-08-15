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

    public void BuyItem(string itemName)
    {
        ShopItem item = m_shopItems.Find(shopItem => shopItem.Name == itemName);
        if (item != null && EconomyManager.Instance.GetPlayerMoney() >= item.Cost)
        {
            EconomyManager.Instance.SpendMoney(item.Cost);

            if (item.ItemType == ItemType.Buildable)
            {
                InventoryManager.Instance.AddBuildableItem(item.Name, item.Prefab, item.Quantity);
            }
            else if(item.ItemType == ItemType.Ammo)
            {
                InventoryManager.Instance.AddAmmoItem(item.Name, item.Prefab, item.Quantity);
            }
            else if (item.ItemType == ItemType.Weapon)
            {
                InventoryManager.Instance.AddWeaponItem(item.Prefab);
            }
            else
            {
                InventoryManager.Instance.AddConsumeableItem(item.Name, item.Prefab, item.Quantity);
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
}
