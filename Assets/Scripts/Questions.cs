using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    public class Questions : MonoBehaviour
    {
        public static Questions Instance;

        // Словарь с вопросами, где ключ - это строка (вопрос), а значение - KeyCode
        public Dictionary<string, KeyCode> questions = new Dictionary<string, KeyCode>()
        {
            { "Когда был первый полёт человека в космос?", KeyCode.Keypad1 },
            { "Во сколько начинается утренняя пробежка?", KeyCode.Keypad1 },
            { "Нужно ли сдавать экзамен для получения водительских прав?", KeyCode.Keypad2 },
            { "Какой самый популярный вид спорта в мире?", KeyCode.Keypad2 },
            { "Сколько длится курс лечения антибиотиками?", KeyCode.Keypad3 },
            { "Какое озеро является самым глубоким в мире?", KeyCode.Keypad3 },
            { "Где находится Эйфелева башня?", KeyCode.Keypad4 },
            { "Как зовут главного героя романа 'Война и мир'?", KeyCode.Keypad4 },
            { "Сколько планет в Солнечной системе?", KeyCode.Keypad5 },
            { "Когда был основан Санкт-Петербург?", KeyCode.Keypad5 },
            { "Что нужно сделать для получения загранпаспорта?", KeyCode.Keypad6 },
            { "Какой цвет имеет океанская вода на большой глубине?", KeyCode.Keypad6 },
            { "Какая страна первой отправила человека на Луну?", KeyCode.Keypad7 },
            { "Какие овощи используются в борще?", KeyCode.Keypad7 },
            { "Какое музыкальное произведение написал Петр Чайковский?", KeyCode.Keypad8 },
            { "В каком году произошла Октябрьская революция?", KeyCode.Keypad8 }
        };

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }
    }
}
