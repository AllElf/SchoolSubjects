using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractMap : MonoBehaviour
{
    [SerializeField] Timer _timer;
    [SerializeField] Text _text;
    [SerializeField] float _count;
    void Start()
    {
        _timer = GameObject.FindObjectOfType<Timer>();
    }

    
    public void CountPlus()
    {
        _count++;
    }
}
