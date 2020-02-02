using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoPanelController : MonoBehaviour
{

    private Text _area;
    private Text _gdp;
    private Text _population;
    private RectTransform _rectTransform;
    private bool _active = false;
    private float _uiSpeed = 2000;
    private float _uiOffset = 400;
    private float _uiY = 0;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _area = transform.Find("Area").GetComponent<Text>();
        _gdp = transform.Find("GDP").GetComponent<Text>();
        _population = transform.Find("Population").GetComponent<Text>();
        //_rectTransform.position = new Vector2(_uiOffset, _rectTransform.position.y);
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
        Unactive();
    }

    public void Activate(string name, int area, int gdp, int population)
    {
        _active = true;
        //gameObject.SetActive(true);
        _area.text =  string.Format("Площадь {0} КМ2", area);
        _gdp.text = string.Format("ВВП {0} трлн. долл", gdp);
        _population.text = string.Format("Население {0}", population);
    }

    public void Unactive()
    {
        //_area.text = string.Empty;
        //_gdp.text = string.Empty;
        //_population.text = string.Empty;
        _active = false;
        //gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
