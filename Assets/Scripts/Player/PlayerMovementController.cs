using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform m_cameraTransform;

    private CharacterController m_characterController;
    private Vector3 m_velocity;
    private bool m_isGrounded;

    private float m_xRotation = 0f;

    void Start()
    {
        m_stats = GetComponent<PlayerStats>();
        m_characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_normalSpeed = m_speed;
        m_sprintSpeed = m_speed * 2f;
    }

    void Update()
    {
        if (m_isGrounded && !Input.GetButton("Sprint"))
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
    }

    private void HandleMovement()
    {
        m_isGrounded = m_characterController.isGrounded;

        if (m_isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        m_characterController.Move(move * m_speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && m_isGrounded)
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
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        m_cameraTransform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleSprint()
    {
        if (Input.GetButton("Sprint") && m_isGrounded)
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
}
