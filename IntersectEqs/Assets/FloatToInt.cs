using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToInt : MonoBehaviour
{
    public float _in = -1.0f;
    public int _out;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        _out = (int)((float)_in * (float)0x7FFFFFFF); 
	}
}
