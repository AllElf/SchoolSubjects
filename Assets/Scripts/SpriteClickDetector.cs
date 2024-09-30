using UnityEngine; // ������������ ��� �������������� � ������� Unity

public class SpriteClickDetector : MonoBehaviour // ����� ��� ��������� ������ �� �������
{
    private SpriteRenderer spriteRenderer; // ������ �� ��������� SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // �������� ��������� SpriteRenderer, ������������� � ���� �� �������
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // ���������, ��� �� ���� ���� (����� ������ ����)
        {
            // �������� ������� ���������� ����� ����
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0; // �������� Z ����������, ��� ��� �������� ������� � 2D

            // ��������, ��������� �� ���� � �������� ������ �������
            if (spriteRenderer.bounds.Contains(worldPoint))
            {
                Debug.Log("���� �� �������!"); // ������� ��������� � �������, ���� ���� ������ ������ �������
            }
            else
            {
                Debug.Log("���� �� ��������� �������!"); // ������� ��������� � �������, ���� ���� �� ��������� �������
            }
        }
    }
}
