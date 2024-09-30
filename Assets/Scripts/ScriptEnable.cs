using System.Collections;
using UnityEngine;

public class ScriptEnable : MonoBehaviour
{
    [SerializeField] MathGame _mathGame;
    private void Start()
    {
        StartCoroutine(EnableScript());
    }

    IEnumerator EnableScript()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (_mathGame.enabled == false)
            {
                _mathGame.enabled = true;
            }
        }
        
    }
}
