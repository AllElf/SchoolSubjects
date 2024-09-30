using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image _image;
    [SerializeField] float _seconds;
    public float fillAmounT = 1;
    [SerializeField] float _speed = 0.1f;
    void Start()
    {
        _seconds = _speed / _seconds;
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        while (true)
        {
            fillAmounT -= _seconds;
            yield return new WaitForSeconds(_speed);
            _image.fillAmount = fillAmounT;
            if (fillAmounT <= 0)
            {
                fillAmounT = 1;

            }
        }
    }
}
