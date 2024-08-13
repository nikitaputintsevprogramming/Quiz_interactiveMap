using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_config : MonoBehaviour
{
    private void Start()
    {
        //dropdown.value = PlayerPrefs.GetInt("fps");
        Application.targetFrameRate = 60;
    }
}
