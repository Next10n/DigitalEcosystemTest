using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UICountryListController : MonoBehaviour
{
    private MainController _mainController;
    private bool _active = false;
    private RectTransform _rectTransform;
    private float _uiSpeed = 2000;
    private float _uiOffset = -1155;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainController = GameObject.Find("MainController").GetComponent<MainController>();
        _rectTransform.position = new Vector2(_uiOffset, _rectTransform.position.y);
    }

    private void FixedUpdate()
    {
        if (!_active)
            _rectTransform.anchoredPosition = Vector2.MoveTowards(
                _rectTransform.anchoredPosition,
                new Vector2(_uiOffset, _rectTransform.anchoredPosition.y),
                Time.deltaTime * _uiSpeed);
        if (_active)
            _rectTransform.anchoredPosition = Vector2.MoveTowards(
                _rectTransform.anchoredPosition,
                new Vector2(0, 0),
                Time.deltaTime * _uiSpeed);
    }

    private void Start()
    {
        Unactivate();
    }

    public void Activate()
    {
        _active = true;
    }

    public void Unactivate()
    {
        _active = false;
    }



}
