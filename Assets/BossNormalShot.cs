using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalShot : MonoBehaviour
{

    [SerializeField] private const float m_ProjectileSpeed = 5.0f;

    private const string PLAYER_TAG = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            Destroy(other.gameObject);

            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * m_ProjectileSpeed * Time.deltaTime);
    }
}
