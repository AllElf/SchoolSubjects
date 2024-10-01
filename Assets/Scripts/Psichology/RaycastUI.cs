using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaycastUI : MonoBehaviour
{
    // ������ �� ��������� GraphicRaycaster, ������� ����� �������������� ��� RayCast
    public GraphicRaycaster raycaster;
    // ������ �� EventSystem, ����������� ��� ������ � PointerEventData
    public EventSystem eventSystem;
    // ���������� ��� �������� ����� �������, � ������� ���������� ���
    public string hitObjectName;
    //// ���������� ��� �������� ������ ���������� Text, ���� �� ����
    //[Multiline]
    //[SerializeField] string hitObjectText;

    void Start()
    {
        // ������� ����� ���������� � �����, ���� ��� �� ����������� � ����������
        if (raycaster == null)
        {
            raycaster = FindObjectOfType<GraphicRaycaster>();
        }
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
    }

    void Update()
    {
        // ���������, ���� �� ������ ����� ������ ����
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // ������� ����� ������ PointerEventData � ������� �������� ����
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            // ������ ��� �������� ����������� RayCast
            List<RaycastResult> results = new List<RaycastResult>();
            // ��������� RayCast � �������������� GraphicRaycaster
            raycaster.Raycast(pointerEventData, results);

            // ���������, ��� ������ �� ������
            if (results.Count > 0)
            {
                // �������� ������ ������� ������
                RaycastResult firstResult = results[0];
                // ��������� ��� ������� � ����������
                hitObjectName = firstResult.gameObject.name;
                //// ���������, ���� �� � ������� ��������� Text
                //if (firstResult.gameObject.GetComponent<Text>() != null)
                //{
                //    // ��������� ����� ���������� � ����������
                //    hitObjectText = firstResult.gameObject.GetComponent<Text>().text;
                //}
                // ������� ��� ������� ������� � �������
                //Debug.Log(firstResult.gameObject.name);
            }
        }
    }
}
