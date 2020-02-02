using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private GameObject[] _countries;
    private CountryController[] _countryControllers;
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = GameObject.Find("Events").GetComponent<EventManager>();
        _countries = GameObject.FindGameObjectsWithTag("Country");
        _countryControllers = new CountryController[_countries.Length];
        for (int i = 0; i < _countries.Length; i++)
        {
            _countryControllers[i] = _countries[i].AddComponent<CountryController>();
        }
        _eventManager.activeIvent.AddListener(OnActive);
    }


    private void OnActive(string name, int area, int gdp, int population)
    {
        for (int i = 0; i < _countries.Length; i++)
        {
            if(_countryControllers[i].GetComponent<Country>().CountryName != name)
            {
                _countryControllers[i].UnactivateCity();
            }
        }
    }

    public int CountSelected()
    {
        int count = 0;
        foreach (CountryController countryController in _countryControllers)
        {
            if (countryController.IsSelected())
            {
                count++;
            }
        }
        
        return count;
    }

    public Country GetFirstActiveCountry()
    {
        foreach (CountryController countryController in _countryControllers)
        {
            if (countryController.IsActive())
            {
                return countryController._country;
            }
        }
        return null;
    }


    public int CountActive()
    {
        int count = 0;
        foreach (CountryController countryController in _countryControllers)
        {
            if (countryController.IsActive())
            {
                count++;
            }
        }
        return count;
    }

    public List<Country> GetSelectedCountries()
    {
        List<Country> countries = new List<Country>();
        foreach (CountryController countryController in _countryControllers)
        {
            if (countryController.IsSelected())
            {
                countries.Add(countryController._country);
            }
        }
        return countries;
    }

    public void UnselectAll()
    {
        for (int i = 0; i < _countries.Length; i++)
        {
            if (_countryControllers[i].GetComponent<Country>().CountryName != name)
            {
                _countryControllers[i].Unselected();
            }
        }
    }

    private void OnApplicationQuit()
    {
        _eventManager.activeIvent.RemoveListener(OnActive);
    }

}
