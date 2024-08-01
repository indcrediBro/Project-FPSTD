using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private WeaponBase[] inventory;
    private int index = 0;
    private WeaponBase equipped;

    private void Start()
    {
        inputManager = InputManager.Instance;
        EquipWeapon(index);
    }

    private void Update()
    {
        if (equipped == null) return;

        if (inputManager.m_SwitchWeaponInput > 0)
            SwitchToWeapon(1);
        if (inputManager.m_SwitchWeaponInput < 0)
            SwitchToWeapon(-1);
    }

    private void SwitchToWeapon(int change)
    {
        index = (index + change + inventory.Length) % inventory.Length;
        EquipWeapon(index);
    }
    private void EquipWeapon(int _index)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].gameObject.SetActive(false);

            if (i == _index)
            {
                equipped = inventory[i];
                equipped.gameObject.SetActive(true);
            }
        }
    }
}
