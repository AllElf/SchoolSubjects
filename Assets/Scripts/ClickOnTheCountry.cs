using UnityEngine;

public class ClickOnTheCountry : MonoBehaviour
{
    public string _nameCityOrCountry;
    GeographyUzbekistan geographyUzbekistan;

    private void Start()
    {
        geographyUzbekistan = GameObject.FindObjectOfType<GeographyUzbekistan>();
    }
    private void OnMouseDown()
    {
        geographyUzbekistan._currentCity = _nameCityOrCountry;
        Debug.Log(_nameCityOrCountry);
        geographyUzbekistan.Check();
    }
}
