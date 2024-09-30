using UnityEngine; // ������������ ��� �������������� � ������� Unity
using UnityEngine.EventSystems; // ������������ ��� ��������� ������� �����

public class PixelPerfectClickSpriteRenderer : MonoBehaviour, IPointerClickHandler // ����� ��������� ��������� IPointerClickHandler ��� ��������� ������ ����
{
    [SerializeField] private SpriteRenderer spriteRenderer; // ������ �� ��������� SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // �������� ��������� SpriteRenderer, ������������� � ���� �� �������
    }

    public void OnPointerClick(PointerEventData eventData) // ����� ���������� ��� ����� ���� �� ������
    {
        CheckTransparency(eventData); // ����� ������ ��� �������� ������������
    }

    private void CheckTransparency(PointerEventData eventData) // ����� ��� �������� ������������ ������� ��� ��������
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position); // ����������� ���������� ������ � ������� ����������
        Vector2 localPos = transform.InverseTransformPoint(worldPos); // ����������� ������� ���������� � ��������� ����������

        // ����������� ��������� ���������� � UV ����������
        Vector2 uv = new Vector2(
            localPos.x / spriteRenderer.sprite.bounds.size.x + spriteRenderer.sprite.bounds.extents.x,
            localPos.y / spriteRenderer.sprite.bounds.size.y + spriteRenderer.sprite.bounds.extents.y);

        // ����������� UV ���������� � ���������� ���������� ��������
        Texture2D texture = spriteRenderer.sprite.texture;
        Rect rect = spriteRenderer.sprite.rect;
        uv.x = rect.x + uv.x * rect.width;
        uv.y = rect.y + uv.y * rect.height;

        // �������� ���� ������� �������� �� ������������ �����������
        Color color = texture.GetPixel((int)uv.x, (int)uv.y);

        // ���������, ������������ �� �������
        if (color.a > 0) // ���� �����-����� ������� ������ 0
        {
            Debug.Log("������������!"); // ������� ��������� � �������
        }
        else // ���� �����-����� ������� ����� 0
        {
            Debug.Log("����������!"); // ������� ��������� � �������
        }
    }
}
