using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerDodge : MonoBehaviour
{
    [SerializeField] private float m_dodgeDistance = 5f;
    [SerializeField] private float m_dodgeCooldown = 1f;
    private float m_dodgeDuration = 0.2f;
    private bool m_canDodge = true;
    public bool CanDodge() { return m_canDodge; }
    [SerializeField] private float m_dutchAngle = 1f;

    [SerializeField] private bool m_useStamina;
    [SerializeField] private float m_staminaCost = 3f;

    private CharacterController m_characterController;
    private PlayerStats m_stats;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
        m_characterController = m_stats.GetCharacterControllerComponent();
    }

    public void Dodge(Vector2 _moveInput, bool _dodgeInput)
    {
        if (GameReferences.Instance.m_IsPaused) return;

        if (_dodgeInput && m_canDodge)
        {
            if (m_useStamina && m_stats.GetStamina() > m_staminaCost)
            {
                m_stats.GetPlayerStaminaComponent().UseStamina(m_staminaCost);
                StartCoroutine(CO_DodgeRoutine(_moveInput));
            }
            else
            {
                StartCoroutine(CO_DodgeRoutine(_moveInput));
            }
        }
    }

    private IEnumerator CO_DodgeRoutine(Vector2 _moveInput)
    {
        m_canDodge = false;

        // Make a GetDodgeDirection function
        Vector3 dodgeDirection = _moveInput.magnitude > 0 ? transform.TransformDirection(new Vector3(_moveInput.x, 0f, _moveInput.y).normalized) : -transform.forward;

        float elapsedTime = 0f;

        float targetDutchAngle = m_dutchAngle * _moveInput.x;
        StartCoroutine(SmoothDutchTilt(targetDutchAngle, 0.1f));

        while (elapsedTime < m_dodgeDuration)
        {
            // Extract this into function
            float step = (m_dodgeDistance / m_dodgeDuration) * Time.deltaTime;
            m_characterController.Move(dodgeDirection * step);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(SmoothDutchTilt(0, 0.1f));

        // You can possibly cache WaitForSeconds
        yield return new WaitForSeconds(m_dodgeCooldown - m_dodgeDuration);
        m_canDodge = true;
    }

    private IEnumerator SmoothDutchTilt(float _targetDutch, float _duration)
    {
        float initialDutch = m_stats.GetVirtualCameraComponent().m_Lens.Dutch;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            m_stats.GetVirtualCameraComponent().m_Lens.Dutch = Mathf.Lerp(initialDutch, _targetDutch, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_stats.GetVirtualCameraComponent().m_Lens.Dutch = _targetDutch;
    }
}