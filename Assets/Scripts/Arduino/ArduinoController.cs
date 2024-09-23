using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using UI.Pagination;
using System.Linq;


namespace Quiz
{
    public class ArduinoController : MonoBehaviour
    {
        SerialPort serialPort;

        [SerializeField] private PagedRect pagedRect;
        [SerializeField] private Page currentPage;

        void Start()
        {
            string com_number = FindObjectOfType<COMFromJSON>().COM_number.ToString();
            serialPort = new SerialPort("COM"+ com_number, 9600);  // �������� "COM3" �� ��������������� COM-���� ������ Arduino
            print("COM" + com_number + "!!!!!!!!!!!!!!!!!!!!!!!!");
            // �������� ����������������� �����
            serialPort.Open();
            serialPort.ReadTimeout = 50;
        }

        //private void InitializeCom()
        //{

        //}

        //void Update()
        //{
        //    // ��������, �������� ������� '1', ����� ������ ������� "Space"
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        SendSignal("1");
        //    }
        //    // �������� ������� '0', ����� �������� ������� "Space"
        //    if (Input.GetKeyUp(KeyCode.Space))
        //    {
        //        SendSignal("0");
        //    }
        //}

        //много много проверок (глаза боятся - руки делают)
        public void SubmitKeyCode()
        {
            pagedRect = FindObjectOfType<PagedRect>(true);
            //currentPage = pagedRect.GetComponentInChildren<Page>(false);
            currentPage = pagedRect.transform.GetChild(0).GetChild(pagedRect.CurrentPage-1).GetComponent<Page>();
            if (currentPage == null)
            {
                Debug.LogError("Page component not found in the scene.");
            }
            else
            {
                Debug.Log("Page component found successfully.");
            }
            //PagedRect currentPage = FindObjectOfType<PagedRect>();


            // Получаем компонент Image
            Image imageComponent = currentPage.GetComponentInChildren<Image>();

            // Проверяем, найден ли компонент Image
            if (imageComponent == null)
            {
                Debug.LogError("Image component not found");
                return;
            }

            // Проверяем, есть ли спрайт у Image
            Sprite sprite = imageComponent.sprite;
            if (sprite == null)
            {
                Debug.LogError("Sprite is null in Image component");
                return;
            }
            else
            {
                Debug.Log("найден спрайт: " + sprite.name);
            }

            //// Получаем текстуру из спрайта
            string currentTexture = sprite.name;
            //if (currentTexture == null)
            //{
            //    Debug.LogError("Texture is null in sprite");
            //    return;
            //}
            //else
            //{
            //    Debug.Log("найдена текстура: " currentTexture)
            //}

            //// Проверяем, содержится ли текстура в словаре
            //if (!Questions.Instance.questions.ContainsKey(currentTexture))
            //{
            //    Debug.LogError("Texture not found in questions dictionary :: " + currentTexture.name);
            //    return;
            //}

            // Пытаемся получить правильный ответ из словаря
            //Questions.Instance.questions.TryGetValue(currentTexture, out KeyCode correctAnswer);
            KeyCode correctAnswer = Questions.Instance.GetKeyCodeFromFileName(currentTexture);
            Debug.Log(correctAnswer + " ++++++++==");
            // Выводим корректный ответ, если он найден
            if (correctAnswer == KeyCode.None)
            {
                Debug.LogWarning("Correct answer not found for the given texture");
            }
            else
            {
                Debug.Log("Correct answer: " + correctAnswer.ToString() + "--------------------------");
            }
            string CorrectAnswer = correctAnswer.ToString();
            string digitsOnly = new string(CorrectAnswer.Where(char.IsDigit).ToArray());
            Debug.Log(digitsOnly);
            SendSignal(digitsOnly);
        }


        public void SendSignal(string signal)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(signal);
            }
            else
            {
                Debug.LogError("Serial port is not open");
            }
        }

        void OnApplicationQuit()
        {
            // �������� ����������������� ����� ��� ������ �� ����������
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
}