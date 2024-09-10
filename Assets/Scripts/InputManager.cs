using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public delegate void KeyCodeHandler3sec(KeyCode keyCode);
    public static event KeyCodeHandler3sec KeyCode3sec;
    public delegate void KeyCodeHandler(KeyCode keyCode);
    public static event KeyCodeHandler KeyCodeDown;

    //����� ��� ���� ������� �� ��������� 3 ���
    private Dictionary<KeyCode, float> keyPressTimers = new Dictionary<KeyCode, float>();


    private void Update()
    {
        // ���������� ��� ��������� �������
        for (KeyCode key = KeyCode.Keypad0; key <= KeyCode.Keypad8; key++)
        {
            // ���������, ���� ������� ������
            if (Input.GetKeyDown(key))
            {
                Debug.Log($"{key} key was pressed!");
                KeyCodeDown?.Invoke(key);  // ����� ������� ������� �������

                // ��������� ������ ��� ���� �������
                if (!keyPressTimers.ContainsKey(key))
                {
                    keyPressTimers[key] = Time.time;
                }
            }

            // ���������, ���� ������� ������������
            if (Input.GetKey(key))
            {
                // ���� ������ 3 ������� � ������� �������, ������� ������� ���������
                if (keyPressTimers.ContainsKey(key) && Time.time - keyPressTimers[key] >= 3.0f)
                {
                    Debug.Log($"{key} key was held for 3 seconds!");
                    KeyCode3secDowned();

                    KeyCode3sec?.Invoke(key);  // ����� ������� ����� ��������� 3 ������

                    // ������� ���� �� �������, ����� �������� ���������� ������
                    keyPressTimers.Remove(key);
                }
            }

            // ���������� ������, ���� ������� ��������
            if (Input.GetKeyUp(key))
            {
                if (keyPressTimers.ContainsKey(key))
                {
                    keyPressTimers.Remove(key);
                }
            }
        }
    }

    public bool KeyCode3secDowned()
    {
        return true;
    }
}
