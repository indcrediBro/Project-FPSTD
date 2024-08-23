using UnityEngine;
using System.Collections;
public class Bow : Weapon
{
    //TODO: Have a seperate abstract class for Charged Weapons

    [SerializeField] private Transform m_arrowIdlePosition;
    [SerializeField] private Transform m_arrowChargedPosition;
    [SerializeField] private GameObject m_arrowPrefab;
    [SerializeField] private float m_maxChargeTime = 2f;
    [SerializeField] private float m_maxDamage = 50f;
    [SerializeField] private float m_maxSpeed = 50f;

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

    private void FixedUpdate()
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
        UnloadArrow();
        InventoryManager.Instance.UseAmmoItem("Arrow");
    }

    private void LoadArrow()
    {
        m_currentArrow = Instantiate(m_arrowPrefab, m_arrowIdlePosition);
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
            float shakeIntensity = Mathf.Lerp(0f, 0.001f, chargeRatio);
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            m_currentArrow.transform.localPosition += shakeOffset;
        }
    }

    private void LaunchArrow()
    {
        if (m_currentArrow != null)
        {
            Rigidbody arrowRb = m_currentArrow.GetComponent<Rigidbody>();
            arrowRb.isKinematic = false;

            float chargeRatio = Mathf.Clamp01(m_currentChargeTime / m_maxChargeTime);
            float arrowSpeed = Mathf.Lerp(10f, m_maxSpeed, chargeRatio);
            float arrowDamage = Mathf.Lerp(10f, m_maxDamage, chargeRatio);

            arrowRb.velocity = m_currentArrow.transform.forward * arrowSpeed;

            PlayerWeaponProjectile arrowComponent = m_currentArrow.GetComponent<PlayerWeaponProjectile>();
            arrowComponent.SetDamage(arrowDamage);
        }
    }

    public override void Attack()
    {
        if (InputManager.Instance.m_AttackInput.WasPressedThisFrame() && HasArrows())
        {
            StartCharging();
        }

        if (InputManager.Instance.m_AttackInput.IsPressed() && m_isCharging)
        {
            ContinueCharging();
        }

        if (InputManager.Instance.m_AttackInput.WasReleasedThisFrame() && m_isCharging)
        {
            ReleaseArrow();
        }
    }
}
