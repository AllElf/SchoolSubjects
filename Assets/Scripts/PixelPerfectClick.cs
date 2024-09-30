using UnityEngine; // ������������ ��� �������������� � ������� Unity
using UnityEngine.EventSystems; // ������������ ��� ��������� ������� �����
using UnityEngine.UI; // ������������ ��� ������ � UI ����������

public class PixelPerfectClickImage : MonoBehaviour, IPointerClickHandler // ����� ��������� ��������� IPointerClickHandler ��� ��������� ������ ����
{
    [SerializeField] private Image image; // ������ �� ��������� Image, ������� ����� ������ � ����������

    void Start()
    {
        image = GetComponent<Image>(); // �������� ��������� Image, ������������� � ���� �� �������
    }

    public void OnPointerClick(PointerEventData eventData) // ����� ���������� ��� ����� ���� �� UI �������
    {
        Vector2 localCursor; // ���������� ��� �������� ��������� ��������� �������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform, // �������������� transform � RectTransform
            eventData.position, // ������� ������� ���� �� ������
            eventData.pressEventCamera, // ������, � ������� ���� ����������� �������
            out localCursor); // �������� ���������� ��� ��������� ��������� �������

        localCursor.x += (transform as RectTransform).pivot.x * (transform as RectTransform).rect.width; // ������������� X ���������� � ������ pivot
        localCursor.y += (transform as RectTransform).pivot.y * (transform as RectTransform).rect.height; // ������������� Y ���������� � ������ pivot

        Vector2 uv = new Vector2(
            localCursor.x / (transform as RectTransform).rect.width, // �������������� X ���������� � UV
            localCursor.y / (transform as RectTransform).rect.height); // �������������� Y ���������� � UV

        Texture2D texture = image.sprite.texture; // �������� �������� �� �������
        Rect rect = image.sprite.rect; // �������� ������������� �������
        uv.x = rect.x + uv.x * rect.width; // �������������� UV ��������� � ���������� ���������� �������� �� X
        uv.y = rect.y + uv.y * rect.height; // �������������� UV ��������� � ���������� ���������� �������� �� Y

        Color color = texture.GetPixel((int)uv.x, (int)uv.y); // �������� ���� ������� �������� �� ������������ �����������

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

    // ������������� ����� ��� ������ OnPointerClick
    //public void HandleClick()
    //{
    //    // �������� PointerEventData � �������� �����������
    //    PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
    //    {
    //        position = Input.mousePosition
    //    };

    //    OnPointerClick(pointerEventData); // �������� ����� OnPointerClick
    //}
}
