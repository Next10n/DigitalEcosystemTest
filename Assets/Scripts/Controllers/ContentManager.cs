using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ContentManager : MonoBehaviour
{
    private GameObject _contentPrefab;
    private EventManager _eventManager;
    private List<Country> _countries;
    private MainController _mainController;
    private GameObject _content;

    private void Awake()
    {
        _contentPrefab = Resources.Load("Prefabs/cont") as GameObject;
        _content = GameObject.Find("Content");
        _eventManager = GameObject.Find("Events").GetComponent<EventManager>();
        _mainController = GetComponent<MainController>();
    }

    private void Start()
    {
        _eventManager.selecteIvent.AddListener(AddCountrieToList);
    }


    private void AddCountrieToList(string name, int area, int gdp, int population)
    {
        GameObject temp = Instantiate(_contentPrefab);
        temp.transform.parent = _content.transform;
        temp.tag = "Content";
        temp.name = name;
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.transform.Find("Name").GetComponent<Text>().text = name;
        temp.transform.Find("Area").GetComponent<Text>().text = area.ToString();
        temp.transform.Find("Population").GetComponent<Text>().text = population.ToString();
        temp.transform.Find("GDP").GetComponent<Text>().text = gdp.ToString();
    }

    public void ClearCountryList()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Content"))
        {
            Destroy(go);
        }
        //Debug.Log(GameObject.FindGameObjectsWithTag("Content").Length);
    }


    public void GetSelectedCountries()
    {
        _countries = _mainController.GetSelectedCountries();
    }

    public void Sort(string sortBy, bool ascending)
    {
        GameObject[] contents = GameObject.FindGameObjectsWithTag("Content");
        if(ascending)
            contents = contents.OrderBy(c => Convert.ToInt32(c.transform.Find(sortBy).GetComponent<Text>().text)).ToArray();
        else
            contents = contents.OrderByDescending(c => Convert.ToInt32(c.transform.Find(sortBy).GetComponent<Text>().text)).ToArray();
        for(int i =0; i < contents.Length; i++)
        {
            Debug.Log(contents[i]);
            contents[i].transform.SetSiblingIndex(i);
        }
    }



}
