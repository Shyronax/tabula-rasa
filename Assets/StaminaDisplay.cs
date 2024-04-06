using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaminaDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform m_StaminaRect;
    [SerializeField] private Image m_StaminaImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_StaminaRect.sizeDelta = new Vector2(StaminaManager.Instance.Stamina * 3, 20);
        if(StaminaManager.Instance.Stamina < 33)
        {
            m_StaminaImage.tintColor = Color.red;
        } else
        {
            m_StaminaImage.tintColor = Color.yellow;
        }
    }
}
