using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    [SerializeField] private GameObject m_NormalShot;
    [SerializeField] private GameObject m_Player;
    [SerializeField] private float m_ShotDelay;
    private float m_ShotTime;

    // Start is called before the first frame update
    void Start()
    {
        m_ShotTime = 0;
    }

    private void TrackPlayer()
    {
        transform.rotation = Quaternion.Euler(0, (Mathf.Atan2(m_Player.transform.position.x, m_Player.transform.position.z) * Mathf.Rad2Deg), 0);
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
        if (m_ShotTime < m_ShotDelay)
        {
            m_ShotTime += Time.deltaTime;
        }
        else
        {
            m_ShotTime = 0;
            Instantiate(m_NormalShot, transform.position, transform.rotation);
            Instantiate(m_NormalShot, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y - 25 , 0));
            Instantiate(m_NormalShot, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y + 25 , 0));

        }

    }
}
