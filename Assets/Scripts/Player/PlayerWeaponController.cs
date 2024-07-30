using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInputController m_input;
    [SerializeField] private Weapon[] m_weapons;
    private int m_currentWeaponIndex = 0;
    private Weapon m_currentWeapon;
    private bool m_hasAttacked;

    private void Awake()
    {
        m_input = GetComponent<PlayerStats>().GetPlayerInput(); ;
    }

    private void Start()
    {
        EquipWeapon(m_currentWeaponIndex);
    }

    private void Update()
    {
        if (m_currentWeapon == null) return;

        if (m_input.m_AttackInput.WasPerformedThisFrame())
        {
            m_currentWeapon.StartAttack();
        }

        if (m_input.m_AttackInput.WasReleasedThisFrame())
        {
            m_currentWeapon.StopAttack();
        }

        if (m_input.m_ReloadInput)
        {
            m_currentWeapon.Reload();
        }

        if (m_input.m_SwitchWeaponInput > 0)
        {
            SwitchToNextWeapon();
        }

        if(m_input.m_SwitchWeaponInput < 0)
        {
            SwitchToPreviousWeapon();
        }
    }

    private void EquipWeapon(int _index)
    {
        for (int i = 0; i < m_weapons.Length; i++)
        {
            m_weapons[i].gameObject.SetActive(false);

            if (i == _index)
            {
                m_currentWeapon = m_weapons[i];
                m_currentWeapon.gameObject.SetActive(true);
            }
        }
    }

    private void SwitchToNextWeapon()
    {
        m_currentWeaponIndex = (m_currentWeaponIndex + 1) % m_weapons.Length;
        EquipWeapon(m_currentWeaponIndex);
    }

    private void SwitchToPreviousWeapon()
    {
        m_currentWeaponIndex = (m_currentWeaponIndex - 1 + m_weapons.Length) % m_weapons.Length;
        EquipWeapon(m_currentWeaponIndex);
    }
}
