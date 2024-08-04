using System;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [Serializable]
    public class Item
    {
        public string m_itemName;
        public int m_itemAmount;
    }

    [SerializeField] private Item[] m_Items;

    public void AddOrRemoveItem(string _itemName, int _itemAmount)
    {
        Item item = Array.Find(m_Items, item => item.m_itemName == _itemName);

        if (item == null) return;

        item.m_itemAmount += _itemAmount;
        item.m_itemAmount = Mathf.Clamp(item.m_itemAmount, 0, 100000000);
    }
}
