using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    private PlayerStats m_stats;

    [SerializeField] float m_speed = 10f;
    private float m_normalSpeed;
    private float m_sprintSpeed;
    private float m_sprintTimer = 0f;
    private float m_sprintRate = 0.5f;

    [SerializeField] float m_jumpHeight = 3f;
    [SerializeField] float m_gravity = -9.81f;

    [SerializeField] float m_mouseSensitivity = 500f;
    [SerializeField] private CinemachineVirtualCamera m_camera;

    private CharacterController m_characterController;
    private Vector3 m_velocity;
    private bool m_isGrounded;

    private float m_xRotation = 0f;

    [SerializeField] float m_dutchAngle = 1f;
    [SerializeField] float m_dodgeDistance = 5f;
    [SerializeField] float m_dodgeCooldown = 1f;
    private float m_dodgeDuration = 0.2f;
    private bool m_canDodge = true;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
        m_characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_normalSpeed = m_speed;
        m_sprintSpeed = m_speed * 2f;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (m_isGrounded && !m_stats.GetPlayerInput().m_SprintInput && m_canDodge)
        {
            m_stats.EnableStaminaRegeneration();
        }
        else
        {
            m_stats.DisableStaminaRegeneration();
        }

        HandleSprint();
        HandleMovement();
        HandleMouseLook();
        HandleDodge();
    }

    private void HandleMovement()
    {
        m_isGrounded = m_characterController.isGrounded;

        if (m_isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        Vector2 input = m_stats.GetPlayerInput().m_MoveInput;
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        m_characterController.Move(move * m_speed * Time.deltaTime);

        //Handle Jump
        if (m_stats.GetPlayerInput().m_JumpInput && m_isGrounded)
        {
            if (m_stats.UseStamina(3f) > 0)
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            }
        }

        m_velocity.y += m_gravity * Time.deltaTime;
        m_characterController.Move(m_velocity * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        Vector2 input = m_stats.GetPlayerInput().m_LookInput;
        float mouseX = input.x * m_mouseSensitivity * Time.deltaTime;
        float mouseY = input.y * m_mouseSensitivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        m_camera.transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleSprint()
    {
        if (m_stats.GetPlayerInput().m_SprintInput && m_stats.GetPlayerInput().m_MoveInput.magnitude > 0 && m_isGrounded && m_stats.GetStamina() > 1f)
        {
            m_sprintTimer += Time.deltaTime;
            if (m_sprintTimer > m_sprintRate)
            {
                if (m_stats.UseStamina(1f) > 0)
                {
                    m_speed = m_sprintSpeed;
                    m_sprintTimer = 0f;
                }
                else
                {
                    m_speed = m_normalSpeed;
                }
            }
        }
        else
        {
            m_speed = m_normalSpeed;
        }
    }

    private void HandleDodge()
    {
        if (m_stats.GetPlayerInput().m_DodgeInput && m_canDodge)
        {
            if (m_stats.UseStamina(3f) > 0)
                StartCoroutine(Dodge());
        }
    }

    private IEnumerator Dodge()
    {
        m_canDodge = false;

        Vector3 moveInput = new Vector3(m_stats.GetPlayerInput().m_MoveInput.x, 0f, m_stats.GetPlayerInput().m_MoveInput.y);
        Vector3 dodgeDirection = (moveInput.magnitude > 0) ? transform.TransformDirection(moveInput.normalized) : -transform.forward;

        float elapsedTime = 0f;

        float targetDutchAngle = m_dutchAngle * moveInput.x;
        StartCoroutine(SmoothDutchTilt(targetDutchAngle, 0.1f));

        while (elapsedTime < m_dodgeDuration)
        {
            float step = (m_dodgeDistance / m_dodgeDuration) * Time.deltaTime;
            m_characterController.Move(dodgeDirection * step);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(SmoothDutchTilt(0, 0.1f));

        yield return new WaitForSeconds(m_dodgeCooldown - m_dodgeDuration);
        m_canDodge = true;
    }

    private IEnumerator SmoothDutchTilt(float targetDutch, float duration)
    {
        float initialDutch = m_camera.m_Lens.Dutch;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            m_camera.m_Lens.Dutch = Mathf.Lerp(initialDutch, targetDutch, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_camera.m_Lens.Dutch = targetDutch;
    }
}
