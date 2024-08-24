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
        if (!m_currentWeapon)
        { return; }

        if (GameReferences.Instance.m_IsPaused) { return; }

        if (!m_stats.IsInBuilderMode())
        {
            HandleEquipement();
            HandleWeaponSwitching();
        }
        else
        {
            if (m_currentWeapon && m_currentWeapon.activeInHierarchy)
            {
                UnequipWeapon();
            }
        }
    }

    private void HandleWeaponSwitching()
    {
        float switchValue = InputManager.Instance.m_SwitchWeaponInput.ReadValue<float>();
        if (InputManager.Instance.m_SwitchWeaponInput.WasPerformedThisFrame() && switchValue > 0)
        {
            SwitchToWeapon(1);
        }

        if (InputManager.Instance.m_SwitchWeaponInput.WasPerformedThisFrame() && switchValue < 0)
        {
            SwitchToWeapon(-1);
        }
    }

    private void HandleEquipement()
    {
        if (m_currentWeapon)
        {
            m_currentWeapon.SetActive(true);
        }
        else
        {
            EquipWeapon(m_currentWeaponIndex);
        }
    }

    private void SwitchToWeapon(int change)
    {
        m_currentWeaponIndex = (m_currentWeaponIndex + change + m_allWeapons.Count) % m_allWeapons.Count;
        EquipWeapon(m_currentWeaponIndex);
    }

    private void EquipWeapon(int _index)
    {
        for (int i = 0; i < m_allWeapons.ToArray().Length; i++)
        {
            m_allWeapons[i].SetActive(false);

            if (i == _index)
            {
                m_currentWeapon = m_allWeapons[i];
                m_currentWeapon.SetActive(true);
            }
        }
    }

    private void UnequipWeapon()
    {
        m_currentWeapon.SetActive(false);
    }

    public void AddWeapon(GameObject _weapon)
    {
        Transform newWeapon = _weapon.transform;
        newWeapon.SetParent(m_weaponRootTF);
        newWeapon.localPosition = Vector3.zero;
        newWeapon.localRotation = Quaternion.identity;
        newWeapon.localScale = Vector3.one;
        m_allWeapons.Add(newWeapon.gameObject);
        EquipWeapon(m_allWeapons.IndexOf(newWeapon.gameObject));
    }

    public Weapon GetActiveWeapon()
    {
        return m_currentWeapon.GetComponent<Weapon>();
    }
}
