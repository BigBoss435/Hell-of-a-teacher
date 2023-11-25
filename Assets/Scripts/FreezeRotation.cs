using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{ 
    void Update()
    {
        // Freeze rotation in every direction
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
