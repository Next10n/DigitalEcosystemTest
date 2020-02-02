using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public bool _ascending = true;
    private Sprite _btnUp;
    private Sprite _btnDown;
    private Image _btnImg;
    private ContentManager _contentManager;

    private void Awake()
    {
        _contentManager = GameObject.Find("MainController").GetComponent<ContentManager>();
        _btnImg = GetComponent<Image>();
        _btnUp = Resources.Load<Sprite>("Sprites/BtnUp");
        _btnDown = Resources.Load<Sprite>("Sprites/BtnDown");
        _btnImg.sprite = _btnUp;
    }

    public void SwitchAscending()
    {        
        _ascending = !_ascending;
        if (_ascending)
            _btnImg.sprite = _btnUp;
        else
            _btnImg.sprite = _btnDown;
    }

    public void Sort(string sortBy)
    {
        _contentManager.Sort(sortBy, _ascending);
    }

}
