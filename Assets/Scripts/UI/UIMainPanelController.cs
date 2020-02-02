using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainPanelController : MonoBehaviour
{
    private GameObject _clearBtn;
    private MainController _mainController;
    private UINcounterController _uiNCounterController;
    private UIMainController _uiMainController;

    private void Awake()
    {
        _uiMainController = transform.parent.GetComponent<UIMainController>();
        _mainController = GameObject.Find("MainController").GetComponent<MainController>();
        _uiNCounterController = GameObject.Find("NcountrySelectedPanel").GetComponent<UINcounterController>();
        _clearBtn = transform.Find("ClearListButton").gameObject;
        ClearBtnUnactivate();
    }


    public void OnClearBtnClick()
    {
        _uiNCounterController.Unactive();
        _mainController.UnselectAll();
        Country country = _mainController.GetFirstActiveCountry();
        _uiMainController.RefreshUI(country.CountryName, country.CountryArea, country.CountryGDP, country.CountryPopulation);
        ClearBtnUnactivate();
    }

    public void ClearBtnActivate()
    {
        _clearBtn.SetActive(true);
    }

    public void ClearBtnUnactivate()
    {
        _clearBtn.SetActive(false);
    }


}
