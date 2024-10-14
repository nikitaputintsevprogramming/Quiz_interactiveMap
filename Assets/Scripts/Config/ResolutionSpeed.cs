using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSpeed : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1024, 768, FullScreenMode.FullScreenWindow);
    }
}
