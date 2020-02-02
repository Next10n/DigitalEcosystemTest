using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageController : MonoBehaviour
{
    private Image _completeMark;
    private bool _selecting = false;
    private bool _selected = false;
    private float _selectedSpeedStep = 0.03f;
    private Country _country;
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = GameObject.Find("Events").GetComponent<EventManager>();
        _country = gameObject.GetComponentInParent<Country>();
        _completeMark = GetComponent<Image>();        
    }

    private void FixedUpdate()
    {
        if (_selecting)
        {
            _completeMark.fillAmount += _selectedSpeedStep;
            if (_completeMark.fillAmount == 1)
            {
                Selected();
            }
        }
    }

    public void StartSecting()
    {
        _selecting = true;
    }

    public void CancelSelecting()
    {
        if (!_selected)
        {
            _selecting = false;
            _completeMark.fillAmount = 0;
        }

    }

    public void Selected()
    {
        _selecting = false;
        _selected = true;
        //_selecteIvent.Invoke(transform.GetComponentInParent<Country>().Name);
        _eventManager.selecteIvent.Invoke(_country.CountryName, _country.CountryArea, _country.CountryGDP, _country.CountryPopulation);
        //_countryController.Select();
    }

    public void Unselect()
    {
        _completeMark.fillAmount = 0;
    }



}
