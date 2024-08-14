using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    public class Questions : MonoBehaviour
    {
        public static Questions Instance;

        // ������� � ���������, ��� ���� - ��� ������ (������), � �������� - KeyCode
        public Dictionary<string, KeyCode> questions = new Dictionary<string, KeyCode>()
        {
            { "����� ��� ������ ���� �������� � ������?", KeyCode.Keypad1 },
            { "�� ������� ���������� �������� ��������?", KeyCode.Keypad1 },
            { "����� �� ������� ������� ��� ��������� ������������ ����?", KeyCode.Keypad2 },
            { "����� ����� ���������� ��� ������ � ����?", KeyCode.Keypad2 },
            { "������� ������ ���� ������� �������������?", KeyCode.Keypad3 },
            { "����� ����� �������� ����� �������� � ����?", KeyCode.Keypad3 },
            { "��� ��������� �������� �����?", KeyCode.Keypad4 },
            { "��� ����� �������� ����� ������ '����� � ���'?", KeyCode.Keypad4 },
            { "������� ������ � ��������� �������?", KeyCode.Keypad5 },
            { "����� ��� ������� �����-���������?", KeyCode.Keypad5 },
            { "��� ����� ������� ��� ��������� ��������������?", KeyCode.Keypad6 },
            { "����� ���� ����� ��������� ���� �� ������� �������?", KeyCode.Keypad6 },
            { "����� ������ ������ ��������� �������� �� ����?", KeyCode.Keypad7 },
            { "����� ����� ������������ � �����?", KeyCode.Keypad7 },
            { "����� ����������� ������������ ������� ���� ����������?", KeyCode.Keypad8 },
            { "� ����� ���� ��������� ����������� ���������?", KeyCode.Keypad8 }
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
