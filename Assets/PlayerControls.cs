using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 5.0f;  
    [SerializeField] private float m_TurnSmoothTime = 0.1f; 
    [SerializeField] private float m_DashRange = 50.0f;
    [SerializeField] private float m_DashStaminaCost = 33.3f;
    [SerializeField] private LayerMask m_groundMask;
    [SerializeField] private GameObject m_Boss;
    private Camera m_mainCamera;

    private CharacterController m_Character;
    private float m_TurnSmoothVelocity;

    private bool m_IsImmobile;
    private bool m_IsImmune;

    private Vector2 m_MoveVector;

    #region Initialization  
    private void OnEnable()
    {
        m_IsImmobile = false;
        m_IsImmune = false;
        m_mainCamera = Camera.main;
        m_Character = gameObject.AddComponent<CharacterController>();   
        m_Character.radius = 0.4f;
        StaminaManager.Instance.InitializeStamina();
    }

    private void OnDisable()
    {
        Destroy(m_Character);   
    }
    #endregion

    private void FixedUpdate()  
    {
        Aim();
        Move();
        StaminaManager.Instance.RegenStamina();
    }

    public void ReadMoveInput(InputAction.CallbackContext context)  
    {
        m_MoveVector = context.ReadValue<Vector2>();    
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && StaminaManager.Instance.Stamina >= 33 && (m_MoveVector.x != 0 || m_MoveVector.y != 0))
        {
            Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y);

            m_Character.Move(direction.normalized * m_DashRange * Time.deltaTime);

            StaminaManager.Instance.SpendStamina(m_DashStaminaCost);
        }

    }

    public void ReadAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("charging");
            m_IsImmobile = true;
        }

        if (context.canceled && context.duration > 1)
        {
            Attack();
            m_IsImmobile = false;
        } else if (context.canceled)
        {
            Debug.Log(context.duration);
            m_IsImmobile = false;
        }
    }

    public void Attack()
    {
        Debug.Log("attack");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            hit.collider.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    public void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, m_groundMask))
        {
            // The Raycast hit something, return with the position.
            Debug.Log("raycast got" + hitInfo.point);
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            Debug.Log("raycast didn't hit");
            return (success: false, position: Vector3.zero);
        }
    }

    private void Move() 
    {
        // Find the direction
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y);    

        if (direction.magnitude >= 0.1f && m_IsImmobile == false)    
        {
            // Get direction angle from direction vector
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;  

            // Add camera rotation to move based on camera forward
            targetAngle += Camera.main.transform.eulerAngles.y; 

            // Smooth the angle over time to avoid snappy rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);  

            // Apply smoothed rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);   

            // Convert direction angle into a moving vector
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;  

            // Apply movement
            m_Character.Move(moveDir.normalized * m_Speed * Time.deltaTime);    
        }
    }
}  