using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private bool _pause = false;

   public void Pause()
    {
        _pause = true;
    }

    public void UnPause()
    {
        _pause = false;
    }

    public bool IsPause()
    {
        return _pause;
    }
}
