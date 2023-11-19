using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlipper : MonoBehaviour
{
    //public Transform teacherPlayer;
    Transform mathPlayer;
    private PlayerController player;

    void Start()
    {
        mathPlayer = FindObjectOfType<PlayerStats>().transform;
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
       transform.position = mathPlayer.transform.position;

        Vector3 rotation = transform.rotation.eulerAngles;
        
        if (player.lastMovedVector.x > 0)
        {
            rotation.y = 0f;
        }
        else if (player.lastMovedVector.x < 0)
        {
            rotation.y = -180f;
        }
        
        transform.rotation = Quaternion.Euler(rotation);
    }
}
