using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [System.Serializable]
    public class InventoryItem
    {
        public string Name;
        public GameObject Prefab;
        public int Quantity;
    }

    [SerializeField] private List<InventoryItem> m_inventoryItems;

    public InventoryItem GetSelectedItem(string _itemName)
    {
        return m_inventoryItems.Find(item => item.Name == _itemName && item.Quantity > 0);
    }

    public void UseItem(string _itemName)
    {
        InventoryItem item = GetSelectedItem(_itemName);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
            {
                m_inventoryItems.Remove(item);
            }
        }
    }

    public void AddItem(string _itemName, GameObject _prefab, int _quantity)
    {
        InventoryItem existingItem = m_inventoryItems.Find(item => item.Name == _itemName);
        if (existingItem != null)
        {
            existingItem.Quantity += _quantity;
        }
        else
        {
            InventoryItem newItem = new InventoryItem { Name = _itemName, Prefab = _prefab, Quantity = _quantity };
            m_inventoryItems.Add(newItem);
        }
    }

    public List<InventoryItem> GetInventoryItems()
    {
        return m_inventoryItems;
    }
}
