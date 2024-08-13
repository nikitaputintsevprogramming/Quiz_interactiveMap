using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[ExecuteInEditMode]
public class InputManager : MonoBehaviour
{
    public delegate void ClickAction(KeyCode keyCode);
    public static event ClickAction AddCameraPreset;

    private void Update()
    {
        for (KeyCode key = KeyCode.Keypad0; key <= KeyCode.Keypad8; key++)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log($"{key} key was pressed!");
                AddCameraPreset(key);
            }
        }
    }
}
