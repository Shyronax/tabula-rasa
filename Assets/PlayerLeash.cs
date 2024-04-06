using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeash : MonoBehaviour
{
    [SerializeField] private GameObject m_Platform;
    [SerializeField] private float m_MaxLeashDistance;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the vector from leashOrigin to player
        Vector3 leashVector = transform.position - m_Platform.transform.position;

        // Calculate the distance from leashOrigin to player
        float distanceToPlayer = leashVector.magnitude;

        // If the player is beyond the maximum leash distance
        if (distanceToPlayer > m_MaxLeashDistance)
        {
            // Shorten the leash to the maximum distance
            leashVector = leashVector.normalized * m_MaxLeashDistance;

            // Update player's position to be within the leash range
            transform.position = m_Platform.transform.position + leashVector;
        }
    }
}
