using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private Dictionary<string, GameObject> m_itemPrefabs = new Dictionary<string, GameObject>();
    private List<string> m_inventory = new List<string>();

    public void AddItem(string _itemName, GameObject _itemPrefab)
    {
        if (!m_itemPrefabs.ContainsKey(_itemName))
        {
            m_itemPrefabs.Add(_itemName, _itemPrefab);
        }
        m_inventory.Add(_itemName);
    }

    public void RemoveItem(string _itemName)
    {
        m_inventory.Remove(_itemName);
    }

    public GameObject GetItemPrefab(string _itemName)
    {
        return m_itemPrefabs.ContainsKey(_itemName) ? m_itemPrefabs[_itemName] : null;
    }

    public List<string> GetInventoryItems()
    {
        return m_inventory;
    }
}
