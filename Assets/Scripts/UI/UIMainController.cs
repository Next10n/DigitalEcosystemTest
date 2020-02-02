using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainController : MonoBehaviour
{
    private EventManager _eventManager;
    private MainController _mainController;
    private UIMainPanelController _uiMainPanelController;
    private UIInfoPanelController _uiInfoPanelController;
    private UINcounterController _uiNCounterController;
    private UICountryListController _uiCountryListController;
    private TimeController _timeController;

    private void Awake()
    {
        _timeController = GameObject.Find("MainController").GetComponent<TimeController>();
        _eventManager = GameObject.Find("Events").GetComponent<EventManager>();
        _uiNCounterController = GameObject.Find("NcountrySelectedPanel").GetComponent<UINcounterController>();
        _uiMainPanelController = transform.Find("MainPanel").GetComponent<UIMainPanelController>();
        _uiInfoPanelController = transform.Find("InfoPanel").GetComponent<UIInfoPanelController>();
        _uiCountryListController = transform.Find("CountryList").GetComponent<UICountryListController>();
        _mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    private void Start()
    {
        _eventManager.activeIvent.AddListener(RefreshUI);
        _eventManager.selecteIvent.AddListener(RefreshUI);
    }


    public void RefreshUI(string name, int area, int gdp, int population)
    {
        if(_mainController.CountSelected() > 0)
        {
            _uiNCounterController.Activate();
            _uiInfoPanelController.Unactive();
            _uiMainPanelController.ClearBtnActivate();
            return;
        }
        if(_mainController.CountActive() > 0)
        {
            _uiInfoPanelController.Activate(name, area, gdp, population);
        }
    }

    public void OnListBtnClick()
    {
        _uiCountryListController.Activate();
        _timeController.Pause();
    }

    public void OnBackBtnClick()
    {
        _uiCountryListController.Unactivate();
        _timeController.UnPause();
    }

    private void OnApplicationQuit()
    {
        _eventManager.activeIvent.RemoveListener(RefreshUI);
        _eventManager.selecteIvent.RemoveListener(RefreshUI);
    }
}
