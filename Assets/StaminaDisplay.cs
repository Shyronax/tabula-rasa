using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaminaDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform StaminaBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StaminaBar.sizeDelta = new Vector2(StaminaManager.Instance.Stamina * 3, 20);
    }
}
