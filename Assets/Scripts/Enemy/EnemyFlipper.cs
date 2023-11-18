using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlipper : MonoBehaviour
{
    //public Transform teacherPlayer;
    Transform mathPlayer;

    public bool flip;
    void Start()
    {
        mathPlayer = FindObjectOfType<PlayerStats>().transform;
    }
    void Update()
    {
        Vector3 scale = transform.localScale;

        if (mathPlayer.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        transform.localScale = scale;   
    }
}
