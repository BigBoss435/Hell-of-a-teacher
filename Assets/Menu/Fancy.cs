using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fancy : MonoBehaviour
{
    public Material FancyMaterial;
    public Material NotFancyMaterial;
    public GameObject Object;
  
    void OnMouseOver()
    {
        Object.GetComponent<SpriteRenderer>().material = FancyMaterial;
    }

    void OnMouseExit()
    {
        Object.GetComponent<SpriteRenderer>().material = NotFancyMaterial;
    }
}
