using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyClass : MonoBehaviour
{
    [Header("Коды класса для 5 классов")]
    [SerializeField] string _class_5A = "Code5AClass1";
    [SerializeField] string _class_5B = "Code5AClass2";
    [SerializeField] string _class_5V = "Code5AClass3";
    [SerializeField] string _class_5G = "Code5AClass4";
    [SerializeField] string _class_5D = "Code5AClass5";
    [Header("Коды класса для 9 классов")]
    [SerializeField] string _class_9A = "Code9AClass1";
    [SerializeField] string _class_9B = "Code9AClass2";
    [SerializeField] string _class_9V = "Code9AClass3";
    [SerializeField] string _class_9G = "Code9AClass4";
    [SerializeField] string _class_9D = "Code9AClass5";
    [Header("InputField для кода класса")]
    [SerializeField] InputField _kode;
    [Header("Панель для кода класса")]
    [SerializeField] GameObject _panelCode;
    [Header("Кнопки")]
    [SerializeField] GameObject _button5Class;
    [SerializeField] GameObject _button9Class;
    [Header("Алфавит класса")]
    [SerializeField] GameObject _A;
    [SerializeField] GameObject _B;
    [SerializeField] GameObject _V;
    [SerializeField] GameObject _G;
    [SerializeField] GameObject _D;

    public void ClassKey()
    {
        if (_kode.text == _class_5A)
        {
            _button9Class.SetActive(false);
            _A.SetActive(true);
            _B.SetActive(false);
            _V.SetActive(false);
            _G.SetActive(false);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_5B)
        {
            _button9Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(true);
            _V.SetActive(false);
            _G.SetActive(false);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_5V)
        {
            _button9Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(false);
            _V.SetActive(true);
            _G.SetActive(false);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_5G)
        {
            _button9Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(false);
            _V.SetActive(false);
            _G.SetActive(true);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_5D)
        {
            _button9Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(false);
            _V.SetActive(false);
            _G.SetActive(false);
            _D.SetActive(true);
            _panelCode.SetActive(false);
        }
        if (_kode.text == _class_9A)
        {
            _button5Class.SetActive(false);
            _A.SetActive(true);
            _B.SetActive(false);
            _V.SetActive(false);
            _G.SetActive(false);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_9B)
        {
            _button5Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(true);
            _V.SetActive(false);
            _G.SetActive(false);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_9V)
        {
            _button5Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(false);
            _V.SetActive(true);
            _G.SetActive(false);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_9G)
        {
            _button5Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(false);
            _V.SetActive(false);
            _G.SetActive(true);
            _D.SetActive(false);
            _panelCode.SetActive(false);
        }
        else if (_kode.text == _class_9D)
        {
            _button5Class.SetActive(false);
            _A.SetActive(false);
            _B.SetActive(false);
            _V.SetActive(false);
            _G.SetActive(false);
            _D.SetActive(true);
            _panelCode.SetActive(false);
        }
        else
        {
            Debug.Log("Введи правильный код класса");
            _kode.text = "Введи правильный код класса";
        }
    }
}
