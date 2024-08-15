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

    [SerializeField] private List<InventoryItem> m_buildableInventoryItems;
    [SerializeField] private List<InventoryItem> m_ammoInventoryItems;
    [SerializeField] private List<InventoryItem> m_consumeableInventoryItems;
    [SerializeField] private PlayerWeaponController m_playerWeaponController;

    public InventoryItem GetBuildableItem(string _itemName)
    {
        return m_buildableInventoryItems.Find(item => item.Name == _itemName && item.Quantity > 0);
    }
    public InventoryItem GetAmmoItem(string _itemName)
    {
        return m_ammoInventoryItems.Find(item => item.Name == _itemName && item.Quantity > 0);
    }
    public InventoryItem GetConsumeableItem(string _itemName)
    {
        return m_consumeableInventoryItems.Find(item => item.Name == _itemName && item.Quantity > 0);
    }

    public List<InventoryItem> GetBuildableInventoryItems()
    {
        return m_buildableInventoryItems;
    }
    public List<InventoryItem> GetAmmoInventoryItems()
    {
        return m_ammoInventoryItems;
    }
    public List<InventoryItem> GetConsumeableInventoryItems()
    {
        return m_consumeableInventoryItems;
    }

    public void AddBuildableItem(string _itemName, GameObject _prefab, int _quantity)
    {
        InventoryItem existingItem = m_buildableInventoryItems.Find(item => item.Name == _itemName);
        if (existingItem != null)
        {
            existingItem.Quantity += _quantity;
        }
        else
        {
            InventoryItem newItem = new InventoryItem { Name = _itemName, Prefab = _prefab, Quantity = _quantity };
            m_buildableInventoryItems.Add(newItem);
        }
    }
    public void AddAmmoItem(string _itemName, GameObject _prefab, int _quantity)
    {
        InventoryItem existingItem = m_ammoInventoryItems.Find(item => item.Name == _itemName);
        if (existingItem != null)
        {
            existingItem.Quantity += _quantity;
        }
        else
        {
            InventoryItem newItem = new InventoryItem { Name = _itemName, Prefab = _prefab, Quantity = _quantity };
            m_ammoInventoryItems.Add(newItem);
        }
    }
    public void AddConsumeableItem(string _itemName, GameObject _prefab, int _quantity)
    {
        InventoryItem existingItem = m_consumeableInventoryItems.Find(item => item.Name == _itemName);
        if (existingItem != null)
        {
            existingItem.Quantity += _quantity;
        }
        else
        {
            InventoryItem newItem = new InventoryItem { Name = _itemName, Prefab = _prefab, Quantity = _quantity };
            m_consumeableInventoryItems.Add(newItem);
        }
    }
    public void AddWeaponItem(GameObject _prefab)
    {
        GameObject newWeapon = Instantiate(_prefab);
        m_playerWeaponController.AddWeapon(newWeapon);
    }

    public void UseBuildableItem(string _itemName)
    {
        InventoryItem item = GetBuildableItem(_itemName);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
            {
                m_buildableInventoryItems.Remove(item);
            }
        }
    }
    public void UseAmmoItem(string _itemName)
    {
        InventoryItem item = GetAmmoItem(_itemName);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
            {
                m_ammoInventoryItems.Remove(item);
            }
        }
    }
    public void UseConsumeableItem(string _itemName)
    {
        InventoryItem item = GetAmmoItem(_itemName);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
            {
                m_consumeableInventoryItems.Remove(item);
            }
        }
    }



}
