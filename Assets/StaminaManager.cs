using UnityEngine;

public class StaminaManager
{
    #region Singleton
    private static StaminaManager m_Instance;

    public static StaminaManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new StaminaManager();
            }

            return m_Instance;
        }
    }
    #endregion

    [SerializeField] private float m_MaxStamina = 100.0f;
    [SerializeField] private float m_StaminaRegen = 0.5f;
    public float Stamina { get; private set; }

    public void InitializeStamina()
    {
        Stamina = m_MaxStamina;
    }
    public void RegenStamina()
    {
        Stamina += m_StaminaRegen;
        if (Stamina >= m_MaxStamina)
        {
            Stamina = m_MaxStamina;
        }
    }

    public void SpendStamina(float cost) 
    {
        Stamina -= cost;
    }
}
