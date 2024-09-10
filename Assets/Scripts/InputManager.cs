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

    //Нужно еще одно соыбтие по истечению 3 сек
    private Dictionary<KeyCode, float> keyPressTimers = new Dictionary<KeyCode, float>();


    private void Update()
    {
        // Перебираем все возможные клавиши
        for (KeyCode key = KeyCode.Keypad0; key <= KeyCode.Keypad8; key++)
        {
            // Проверяем, если клавиша нажата
            if (Input.GetKeyDown(key))
            {
                Debug.Log($"{key} key was pressed!");
                KeyCodeDown?.Invoke(key);  // Вызов события нажатия клавиши

                // Запускаем таймер для этой клавиши
                if (!keyPressTimers.ContainsKey(key))
                {
                    keyPressTimers[key] = Time.time;
                }
            }

            // Проверяем, если клавиша удерживается
            if (Input.GetKey(key))
            {
                // Если прошло 3 секунды с момента нажатия, вызовем событие удержания
                if (keyPressTimers.ContainsKey(key) && Time.time - keyPressTimers[key] >= 3.0f)
                {
                    Debug.Log($"{key} key was held for 3 seconds!");
                    KeyCode3secDowned();

                    KeyCode3sec?.Invoke(key);  // Вызов события после удержания 3 секунд

                    // Удаляем ключ из словаря, чтобы избежать повторного вызова
                    keyPressTimers.Remove(key);
                }
            }

            // Сбрасываем таймер, если клавиша отпущена
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
