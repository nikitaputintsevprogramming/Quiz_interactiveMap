using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void KeyCodeHandler3sec(KeyCode keyCode);
    public static event KeyCodeHandler3sec KeyCode3sec;

    //Нужно еще одно соыбтие по истечению 3 сек

    private void Update()
    {
        for (KeyCode key = KeyCode.Keypad0; key <= KeyCode.Keypad8; key++)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log($"{key} key was pressed!");
                KeyCode3sec(key);
            }
        }
    }
}
