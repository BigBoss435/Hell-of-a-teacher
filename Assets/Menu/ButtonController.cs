using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject panel;
    void OnMouseDown()
    {
        if (panel.activeSelf)
        { panel.SetActive(false); }
        else 
        { panel.SetActive(true); }
    }
}
