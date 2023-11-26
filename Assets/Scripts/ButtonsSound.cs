using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSound : MonoBehaviour
{
    public AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!buttonSound.isPlaying)
        {
            buttonSound.Play();
        }
    }
}
