using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSpeed : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(2160, 3840, FullScreenMode.FullScreenWindow);
    }
}
