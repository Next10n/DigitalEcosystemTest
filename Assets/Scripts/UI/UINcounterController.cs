using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINcounterController : MonoBehaviour
{
    private MainController _mainController;
    private Text _nCountText;
    private EventManager _eventManager;
    private bool _active = false;
    private RectTransform _rectTransform;
    private float _uiSpeed = 2000;
    private float _uiOffset = 400;
    private float _uiY = 0;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainController = GameObject.Find("MainController").GetComponent<MainController>();
        _nCountText = transform.Find("Ncount").gameObject.GetComponent<Text>();
        _eventManager = GameObject.Find("Events").GetComponent<EventManager>();
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
                new Vector2(0, _uiY),
                Time.deltaTime * _uiSpeed);
    }

    private void Start()
    {
        _eventManager.selecteIvent.AddListener(Refresh);
        Unactive();
    }



    public void Activate()
    {
        _mainController.CountSelected();
        _active = true;
        //gameObject.SetActive(true);
    }

    public void Refresh(string name, int area, int gdp, int population)
    {
        _nCountText.text = "Выбрано " + _mainController.CountSelected().ToString() + " стран(a)";
    }

    public void Unactive()
    {
        _active = false;
        //gameObject.SetActive(false);
    }
}

