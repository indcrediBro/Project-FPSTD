using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerStats m_stats;
    [SerializeField] private Transform m_weaponRootTF;
    [SerializeField] private List<GameObject> m_allWeapons;
    private int m_currentWeaponIndex = 0;
    private GameObject m_currentWeapon;

    private void Start()
    {
        EquipWeapon(m_currentWeaponIndex);
    }

    private void Update()
    {
        if (!m_stats.IsInBuilderMode() && m_currentWeapon)
        {
            m_currentWeapon.SetActive(true);

            if (InputManager.Instance.m_SwitchWeaponInput > 0)
            {
                SwitchToWeapon(1);
            }

            if (InputManager.Instance.m_SwitchWeaponInput < 0)
            {
                SwitchToWeapon(-1);
            }
        }
        else
        {
            if (m_stats.IsInBuilderMode())
            {
                m_currentWeapon.SetActive(false);
                if (InputManager.Instance.m_SwitchWeaponInput < 0)
                {
                    BuildManager.Instance.SwitchToBuildable(-1);
                }
                if (InputManager.Instance.m_SwitchWeaponInput > 0)
                {
                    BuildManager.Instance.SwitchToBuildable(1);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_stats.IsInBuilderMode())
            {
                BuildManager.Instance.UnequipCurrentBuildable();
                m_stats.SetBuilderMode(false);
            }
            else
            {
                BuildManager.Instance.EquipLastBuildable();
                m_stats.SetBuilderMode(true);
            }
        }
    }

    private void SwitchToWeapon(int change)
    {
        m_currentWeaponIndex = (m_currentWeaponIndex + change + m_allWeapons.Count - 1) % m_allWeapons.Count - 1;
        EquipWeapon(m_currentWeaponIndex);
    }

    private void EquipWeapon(int _index)
    {
        for (int i = 0; i < m_allWeapons.Count - 1; i++)
        {
            m_allWeapons[i].SetActive(false);

            if (i == _index)
            {
                m_currentWeapon = m_allWeapons[i];
                m_currentWeapon.SetActive(true);
            }
        }
    }

    public void AddWeapon(GameObject _weapon)
    {
        Transform newWeapon = _weapon.transform;
        newWeapon.SetParent(m_weaponRootTF);
        newWeapon.localPosition = Vector3.zero;
        m_allWeapons.Add(newWeapon.gameObject);
        EquipWeapon(m_allWeapons.IndexOf(newWeapon.gameObject));
    }
}
