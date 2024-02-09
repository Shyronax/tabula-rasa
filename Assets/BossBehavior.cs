using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    [SerializeField] private GameObject NormalShot;
    private float ShotDelay;

    // Start is called before the first frame update
    void Start()
    {
        ShotDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShotDelay < 0.5)
        {
            ShotDelay += Time.deltaTime;
        }
        else
        {
            ShotDelay = 0;
            Instantiate(NormalShot, transform.position, Quaternion.identity);
        }

    }
}
