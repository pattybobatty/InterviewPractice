using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public delegate void Clear();
    public Clear _onClear;

    public virtual void OnEnter()
    {

    }
    public virtual void OnInputReceived(char input)
    {
        if (input == 'c')
        {
            OnClearRecieved();
        }
    }

    public void OnClearRecieved()
    {
        Calculator.Instance.OnClearRecieved();
        //if (_onClear != null)
        //{
        //    _onClear();
        //}
    }
}
