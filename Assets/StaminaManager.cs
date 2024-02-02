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

    [SerializeField] private float MaxStamina = 100.0f;
    [SerializeField] private float StaminaRegen = 0.5f;
    public float Stamina { get; private set; }

    public void RegenStamina()
    {
        Stamina += StaminaRegen;
        if (Stamina >= MaxStamina)
        {
            Stamina = MaxStamina;
        }
    }

    public void SpendStamina(float cost) 
    {
        Stamina -= cost;
    }
}
