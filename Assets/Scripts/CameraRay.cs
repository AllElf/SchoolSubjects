using UnityEngine; // ������������ ��� �������������� � ������� Unity

public class MouseRayDrawer : MonoBehaviour // ����� ��� ��������� ���� �� ������ �� ����������� �������
{
    public Camera mainCamera; // ������ �� �������� ������

    void Update()
    {
        
            // �������� �������� ���������� ������� ����
            Vector3 mousePosition = Input.mousePosition;

            // ����������� �������� ���������� � ������� ����������
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); // ����� ���� 100 ������, ������� ����
       


        if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                if (hit.collider != null) // ���� ��� ����� � ���������
                {
                    SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>(); // ���������, ���� �� � ������� ��������� SpriteRenderer
                    if (spriteRenderer != null) // ���� ��������� SpriteRenderer ������
                    {
                        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green); // ���� ����� � SpriteRenderer, ������ ��� ������
                        Debug.Log("Hit a SpriteRenderer!"); // ������� ��������� � �������
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); // �����, ������ ��� �������
                        Debug.Log("Hit something else!"); // ������� ��������� � �������
                    }
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); // ���� �� �� ��� �� �����, ������ ��� �������
                    Debug.Log("Hit nothing!"); // ������� ��������� � �������
                }
            }
        }
    
}
