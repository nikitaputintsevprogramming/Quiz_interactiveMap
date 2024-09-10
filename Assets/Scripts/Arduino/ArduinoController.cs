using System.IO.Ports;
using UnityEngine;

public class ArduinoController : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM4", 9600);  // �������� "COM3" �� ��������������� COM-���� ������ Arduino

    void Start()
    {
        // �������� ����������������� �����
        serialPort.Open();
        serialPort.ReadTimeout = 50;
    }

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
