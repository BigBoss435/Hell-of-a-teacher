using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoader : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("Cutscene", LoadSceneMode.Single);
    }
}
