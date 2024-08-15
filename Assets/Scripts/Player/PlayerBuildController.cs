using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildController : MonoBehaviour
{
    [SerializeField] private PlayerStats m_stats;
    private int m_currentBuildableIndex = 0;

    private void Update()
    {
        //TODO: Use New Input System
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_stats.IsInBuilderMode())
            {
                UnequipCurrentBuildable();
                m_stats.SetBuilderMode(false);
            }
            else
            {
                if (InventoryManager.Instance.GetBuildableInventoryItems().Count != 0)
                    RequipLastBuildable();
                m_stats.SetBuilderMode(true);
            }
        }

        if (!m_stats.IsInBuilderMode() || InventoryManager.Instance.GetBuildableInventoryItems().Count < 1)
        {
            return;
        }

        //if(InventoryManager.Instance.GetBuildableInventoryItems()[m_currentBuildableIndex].Quantity <= 0)
        //{
        //    UnequipCurrentBuildable();
        //}

        if (InputManager.Instance.m_SwitchWeaponInput < 0)
        {
            SwitchToBuildable(-1);
        }
        if (InputManager.Instance.m_SwitchWeaponInput > 0)
        {
            SwitchToBuildable(1);
        }
    }

    public void SwitchToBuildable(int change)
    {
        m_currentBuildableIndex = (m_currentBuildableIndex + change + InventoryManager.Instance.GetBuildableInventoryItems().Count) % InventoryManager.Instance.GetBuildableInventoryItems().Count;
        BuildManager.Instance.SetSelectedBuildable(InventoryManager.Instance.GetBuildableInventoryItems()[m_currentBuildableIndex].Name);
    }

    public void UnequipCurrentBuildable()
    {
        BuildManager.Instance.SetSelectedBuildable("");
    }

    public void RequipLastBuildable()
    {
        BuildManager.Instance.SetSelectedBuildable(InventoryManager.Instance.GetBuildableInventoryItems()[m_currentBuildableIndex].Name);
    }


}
