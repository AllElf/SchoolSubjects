using System.Collections;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] float R, G, B, A;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private void Start()
    {
        R = 1; G = 1; B = 1; A = 1;
    }
    private void OnMouseDown()
    {
        Debug.Log("Нажал");
        StartCoroutine(Click());
        
    }
    //private void Update()
    //{
    //    _spriteRenderer.color = new Color(R, G, B);
    //}
    IEnumerator Click()
    {
        _spriteRenderer.color = new Color(R, G, B);
        R = 0.8f;
        G = 0.8f;
        B = 0.8f;
        yield return new WaitForSeconds(0.1f);
        R = 1f;
        G = 1f;
        B = 1f;
        StopCoroutine(Click());
    }
}
