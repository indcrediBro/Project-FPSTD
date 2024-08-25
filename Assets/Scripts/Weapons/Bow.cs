﻿using UnityEngine;
using System.Collections;
public class Bow : Weapon
{
    //TODO: Have a seperate abstract class for Charged Weapons

    [SerializeField] private Transform m_arrowIdlePosition;
    [SerializeField] private Transform m_arrowChargedPosition;
    [SerializeField] private GameObject m_arrowPrefab;
    [SerializeField] private float m_maxChargeTime = 2f;
    [SerializeField] private float m_maxSpeed = 30f;

    private GameObject m_currentArrow;
    private float m_currentChargeTime = 0f;
    private bool m_isCharging = false;
    private PlayerUIController m_playerUI;

    private void Awake()
    {
        m_playerUI = GetComponentInParent<PlayerUIController>();
    }

    private void UpdateAmmoUI()
    {
        if (m_playerUI)
        {
            if (HasArrows())
            {
                m_playerUI.UpdateAmmoText(InventoryManager.Instance.GetAmmoItem("Arrow").Quantity.ToString());
            }
            else
            {
                m_playerUI.UpdateAmmoText("0");
            }
        }
    }

    private void Update()
    {
        if (HasArrows() && !m_currentArrow)
        {
            LoadArrow();
        }

        Attack();
        UpdateAmmoUI();
    }

    private bool HasArrows()
    {
        if (InventoryManager.Instance.GetAmmoItem("Arrow") != null)
            return InventoryManager.Instance.GetAmmoItem("Arrow").Quantity > 0;
        return false;
    }

    private void StartCharging()
    {
        m_currentChargeTime = 0f;
        m_isCharging = true;
        m_currentArrow.transform.SetParent(m_arrowChargedPosition);
    }

    private void ContinueCharging()
    {
        m_currentChargeTime += Time.deltaTime;
        float chargeRatio = Mathf.Clamp01(m_currentChargeTime / m_maxChargeTime);

        Vector3 targetPosition = Vector3.Lerp(m_currentArrow.transform.localPosition, Vector3.zero, chargeRatio);
        MoveArrowToPosition(targetPosition);

        if (m_currentChargeTime >= m_maxChargeTime)
            ApplyShakeEffect(chargeRatio);
    }

    private void ReleaseArrow()
    {
        m_isCharging = false;
        LaunchArrow();
        InventoryManager.Instance.UseAmmoItem("Arrow");
    }

    private void LoadArrow()
    {
        //m_currentArrow = Instantiate(m_arrowPrefab, m_arrowIdlePosition);

        m_currentArrow = ObjectPoolManager.Instance.GetPooledObject("Ammo_ArrowPlayer");
        m_currentArrow.transform.SetParent(m_arrowIdlePosition);
        m_currentArrow.transform.localPosition = Vector3.zero;
        m_currentArrow.transform.localRotation = Quaternion.identity;
        m_currentArrow.GetComponent<Rigidbody>().isKinematic = true;
        m_currentArrow.GetComponent<Collider>().enabled = false;
        m_currentArrow.SetActive(true);
    }

    private void UnloadArrow()
    {
        m_currentArrow.transform.SetParent(null);
        m_currentArrow = null;
    }

    private void MoveArrowToPosition(Vector3 position)
    {
        if (m_currentArrow != null)
        {
            m_currentArrow.transform.localPosition = position;
        }
    }

    private void ApplyShakeEffect(float chargeRatio)
    {
        if (m_currentArrow != null)
        {
            float shakeIntensity = Mathf.Lerp(0f, 0.002f, chargeRatio);
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            m_currentArrow.transform.localPosition += shakeOffset;
        }
    }

    private void LaunchArrow()
    {
        if (m_currentArrow != null)
        {
            float chargeRatio = Mathf.Clamp01(m_currentChargeTime / m_maxChargeTime);
            float arrowSpeed = Mathf.Lerp(10f, m_maxSpeed, chargeRatio);
            float arrowDamage = Mathf.Lerp(10f, GetCurrentDamage(), chargeRatio);

            PlayerWeaponProjectile arrowComponent = m_currentArrow.GetComponent<PlayerWeaponProjectile>();
            arrowComponent.SetDamage(arrowDamage);

            Rigidbody arrowRb = m_currentArrow.GetComponent<Rigidbody>();
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
            Vector3 directionToLook = worldPosition - arrowRb.position;

            arrowRb.isKinematic = false;
            m_currentArrow.GetComponent<Collider>().enabled = true;

            arrowRb.rotation = Quaternion.LookRotation(directionToLook);
            arrowRb.velocity = m_currentArrow.transform.forward * arrowSpeed;

            UnloadArrow();
        }
    }

    public override void Attack()
    {
        if (InputManager.Instance.m_AttackInput.WasPressedThisFrame() && HasArrows())
        {
            StartCharging();
        }

        if (m_isCharging)
        {
            ContinueCharging();
        }

        if (InputManager.Instance.m_AttackInput.WasReleasedThisFrame() && m_isCharging)
        {
            ReleaseArrow();
        }
    }
}
