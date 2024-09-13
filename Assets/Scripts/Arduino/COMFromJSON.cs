using System.IO;
using UnityEngine;

namespace Quiz
{
    public class COMFromJSON : MonoBehaviour
    {
        //public delegate void ComLoaded();
        //public static event ComLoaded e_ComLoaded;

        // Переменная для хранения числа
        public int COM_number;

        // Структура для парсинга данных из JSON
        [System.Serializable]
        public class COMData
        {
            public int COM;  // Название ключа в JSON
        }

        void OnEnable()
        {
            LoadCOMFromJSON();
        }

        // Метод для чтения файла com.json
        void LoadCOMFromJSON()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "com.json");

            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                COMData data = JsonUtility.FromJson<COMData>(jsonContent);
                COM_number = data.COM;
                Debug.Log("Значение COM: " + COM_number);
                //e_ComLoaded();
            }
            else
            {
                Debug.LogError("Файл com.json не найден.");
            }
        }
    }
}
