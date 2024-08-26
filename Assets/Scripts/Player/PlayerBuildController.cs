using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildController : MonoBehaviour
{
    [SerializeField] private PlayerStats m_stats;
    private int m_currentBuildableIndex = 0;

    private void Update()
    {
        if (InputManager.Instance.m_BuildInput.WasPerformedThisFrame())
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

        float switchValue = InputManager.Instance.m_SwitchWeaponInput.ReadValue<float>();

        if (InputManager.Instance.m_SwitchWeaponInput.WasPerformedThisFrame() && switchValue < 0)
        {
            SwitchToBuildable(-1);
        }
        if (InputManager.Instance.m_SwitchWeaponInput.WasPerformedThisFrame() && switchValue > 0)
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
        BuildManager.Instance.SetSelectedBuildable(GetActiveBuildableName());
    }

    public string GetActiveBuildableName()
    {
        if (InventoryManager.Instance.GetBuildableInventoryItems().Count <= 0)
            return "";
        return InventoryManager.Instance.GetBuildableInventoryItems()[m_currentBuildableIndex].Name;
    }
}
