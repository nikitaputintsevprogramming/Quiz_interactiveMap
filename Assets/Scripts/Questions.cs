using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Quiz
{
    public class Questions : MonoBehaviour
    {
        public static Questions Instance;

        public List<Texture> ListOfTexturesStreamAssets = new List<Texture>();

        // Словарь с вопросами, где ключ - это текстура (вопрос), а значение - KeyCode
        public Dictionary<Texture2D, KeyCode> questions = new Dictionary<Texture2D, KeyCode>();

        // Путь к папке с изображениями
        private string imagePath;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void Start()
        {
            // Путь к папке StreamingAssets/questions
            imagePath = Path.Combine(Application.streamingAssetsPath, "Questions");

            // Загрузить все текстуры и добавить их в словарь
            LoadTexturesFromPath(imagePath);
        }

        private void LoadTexturesFromPath(string path)
        {
            // Проверяем, существует ли папка
            if (!Directory.Exists(path))
            {
                Debug.LogError($"The directory {path} does not exist.");
                return;
            }

            // Получаем все файлы в папке
            string[] files = Directory.GetFiles(path, "*.jpg"); // Допустим, все изображения в формате PNG

            // Загружаем каждую текстуру и добавляем в словарь
            foreach (string file in files)
            {
                print(file);
                // Загрузить текстуру из файла
                byte[] fileData = File.ReadAllBytes(file);
                Texture2D texture = new Texture2D(2, 2); // Создаем временную текстуру
                if (texture.LoadImage(fileData)) // Загружаем изображение в текстуру
                {
                    // Получаем KeyCode из имени файла (например, "question1.png" -> KeyCode.Keypad1)
                    KeyCode keyCode = GetKeyCodeFromFileName(Path.GetFileNameWithoutExtension(file));
                    if (keyCode != KeyCode.None)
                    {
                        questions.Add(texture, keyCode);
                        ListOfTexturesStreamAssets.Add(texture); // для отслеживания в инспекторе
                    }
                    else
                    {
                        Debug.LogWarning($"Could not determine KeyCode for file {file}");
                    }
                }
                else
                {
                    Debug.LogError($"Failed to load texture from file {file}");
                }
            }
        }

        private KeyCode GetKeyCodeFromFileName(string fileName)
        {
            // Преобразование имени файла в KeyCode. Этот метод должен быть настроен в зависимости от вашей логики.
            // Примерная реализация:
            switch (fileName.ToLower())
            {
                case "question_1": return KeyCode.Keypad1;
                case "question_2": return KeyCode.Keypad1;
                case "question_3": return KeyCode.Keypad2;
                case "question_4": return KeyCode.Keypad2;
                case "question_5": return KeyCode.Keypad3;
                case "question_6": return KeyCode.Keypad3;
                case "question_7": return KeyCode.Keypad4;
                case "question_8": return KeyCode.Keypad4;
                case "question_9": return KeyCode.Keypad5;
                case "question_10": return KeyCode.Keypad5;
                case "question_11": return KeyCode.Keypad6;
                case "question_12": return KeyCode.Keypad6;
                case "question_13": return KeyCode.Keypad7;
                case "question_14": return KeyCode.Keypad7;
                case "question_15": return KeyCode.Keypad8;
                case "question_16": return KeyCode.Keypad8;
                // Добавьте остальные случаи
                default: return KeyCode.None;
            }
        }
    }
}
