using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    [SerializeField] private GameObject NormalShot;
    [SerializeField] private GameObject Player;
    private float ShotDelay;

    // Start is called before the first frame update
    void Start()
    {
        ShotDelay = 0;
    }

    private void TrackPlayer()
    {
        transform.rotation = Quaternion.Euler(0, (Mathf.Atan2(Player.transform.position.x, Player.transform.position.z) * Mathf.Rad2Deg), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShotDelay < 0.3)
        {
            ShotDelay += Time.deltaTime;
        }
        else
        {
            ShotDelay = 0;
            Instantiate(NormalShot, transform.position, transform.rotation);
            Instantiate(NormalShot, transform.position, Quaternion.Euler(0, transform.rotation.y - 25 , 0));
            Instantiate(NormalShot, transform.position, Quaternion.Euler(0, transform.rotation.y + 25 , 0));

        }

    }
}
