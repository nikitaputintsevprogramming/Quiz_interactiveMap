using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Pagination
{
    public class StartPageManager : MonoBehaviour
    {
        public void SubcribeOnWaitDownedKey()
        {
            // Подписываемся на событие удержания клавиши в течение 3 секунд
            InputManager.KeyCode3sec += OnKeyCodeHeldFor3Seconds;
        }

        public void UnsubcribeOnWaitDownedKey()
        {
            // Отписываемся от события, чтобы избежать утечек памяти
            InputManager.KeyCode3sec -= OnKeyCodeHeldFor3Seconds;
        }

        // Этот метод будет вызван, когда клавиша удерживается 3 секунды
        public void OnKeyCodeHeldFor3Seconds(KeyCode keyCode)
        {
            Debug.Log($"Key {keyCode} was held for 3 seconds in PagesManager");
            FindObjectOfType<PagedRect>().NextPage();
        }

        public void SubcribeOnKey()
        {
            InputManager.KeyCodeDown += OnKeyCode;
        }

        public void UnsubcribeOnKey()
        {
            InputManager.KeyCodeDown -= OnKeyCode;
        }

        public void OnKeyCode(KeyCode keyCode)
        {
            CheckCorrectKey();
        }

        public void CheckCorrectKey()
        {

        }
    }
}
