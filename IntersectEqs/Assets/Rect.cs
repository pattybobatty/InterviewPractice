using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rect : MonoBehaviour {

    private Vector2 _center;
    private float _width;
    private float _height;
    private float _halfHeight;
    private float _halfWidth;

    public Vector2 Center
    {
        get { return _center; }
    }
    public float Left
    {
        get { return _center.x - _halfWidth; }
    }

    public float Right
    {
        get { return _center.x + _halfWidth; }
    }

    public float Top
    {
        get { return _center.y + _halfHeight; }
    }


    public float Bottom
    {
        get { return _center.y - _halfHeight; }
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        _center = transform.position;
        _halfWidth = transform.localScale.x / 2.0f;
        _halfHeight = transform.localScale.y / 2.0f;
	}
}
