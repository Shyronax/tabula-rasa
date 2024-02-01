using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 5.0f;  
    [SerializeField] private float m_TurnSmoothTime = 0.1f; 
    [SerializeField] private float m_DashRange = 50.0f;

    private CharacterController m_Character;    
    private float m_TurnSmoothVelocity; 

    private Vector2 m_MoveVector;   

    #region Initialization  
    private void OnEnable()
    {
        m_Character = gameObject.AddComponent<CharacterController>();   
        m_Character.radius = 0.4f;  
    }

    private void OnDisable()
    {
        Destroy(m_Character);   
    }
    #endregion

    private void FixedUpdate()  
    {
        Move(); 
    }

    public void ReadMoveInput(InputAction.CallbackContext context)  
    {
        m_MoveVector = context.ReadValue<Vector2>();    
    }   

    public void Dash(){
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y);

        m_Character.Move(direction.normalized * m_DashRange * Time.deltaTime);

    }

    private void Move() 
    {
        // Find the direction
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y);    

        if (direction.magnitude >= 0.1f)    
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