using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CountryController : MonoBehaviour
{
    private GameObject _icon;
    private GameObject _sight;
    private GameObject _completeMark;
    private ObjectController _iconController;
    private ObjectController _sightController;
    private ObjectController _textController;
    private ImageController _imageController;
    private EventManager _eventManager;
    private bool _activated = false;
    private bool _selected = false;
    public Country _country;
    private TimeController _timeController;
    private CameraController _cameraController;



    private void Awake()
    {
        _cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        _eventManager = GameObject.Find("Events").GetComponent<EventManager>();
        _timeController = GameObject.Find("MainController").GetComponent<TimeController>();
        _country = GetComponent<Country>();
        _iconController = gameObject.transform.Find("Icon").gameObject.AddComponent<ObjectController>();
        _sightController = gameObject.transform.Find("Sight").gameObject.AddComponent<ObjectController>();
        _textController = gameObject.transform.Find("CountryName").gameObject.AddComponent<ObjectController>();
        _imageController = gameObject.transform.Find("Canvas").transform.Find("CompleteMark").gameObject.AddComponent<ImageController>();
        _eventManager.selecteIvent.AddListener(Select);

    }

    private void Start()
    {
        Unselected();
        UnactivateCity();
    }




    private void OnMouseUp()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                _imageController.CancelSelecting();
            }
        }
    }

    private void OnMouseDown()
    {
        if (!_timeController.IsPause())
        {
            _cameraController.SetTarget(transform);
            if (!_activated)
            {
                ActivateCity();
            }
            else
            {
                _imageController.StartSecting();
            }
        }
    }

    public void ActivateCity()
    {
        _activated = true;
        _iconController.SetActive(false);
        _sightController.SetActive(true);
        _textController.SetActive(true);
        _eventManager.activeIvent.Invoke(_country.CountryName, _country.CountryArea, _country.CountryGDP, _country.CountryPopulation);
    }

    public void UnactivateCity()
    {
        _activated = false;
        _iconController.SetActive(true);
        _sightController.SetActive(false);
        _textController.SetActive(false);
    }

    public void Select(string name, int area, int gdp, int population)
    {
        if(name == _country.CountryName)
            _selected = true;
    }

    public void Unselected()
    {
        _imageController.Unselect();
        _selected = false;
    }

    public bool IsSelected()
    {
        return _selected;
    }

    public bool IsActive()
    {
        return _activated;
    }

    private void OnApplicationQuit()
    {
        _eventManager.selecteIvent.RemoveListener(Select);
    }
}
