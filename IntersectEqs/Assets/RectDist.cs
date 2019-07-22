using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectDist : MonoBehaviour {

    public Rect a;
    public Rect b;
    // Use this for initialization
    private Color aColor = Color.green;
    private Color bColor = Color.red;
       
    void Start ()
    {
	    if ( a != null && b != null )
        {
            a.GetComponent<Renderer>().material.color = aColor;
            b.GetComponent<Renderer>().material.color = bColor;
        }
	}
	
    float Distance(Rect a, Rect b)
    {

        a.GetComponent<Renderer>().material.color = aColor;
        b.GetComponent<Renderer>().material.color = bColor;
        if (a.Left > b.Right)
        {
            // a is to the right
            if ( a.Bottom > b.Top )
            {
                // a is above
                Vector2 first = new Vector2(a.Left, a.Bottom);
                Vector2 second = new Vector2(b.Right, b.Top);
                Debug.DrawLine(first, second, Color.red, 0.1f);
                Vector2 v = first - second; 
                return (v.magnitude);
            }
            if(a.Top < b.Bottom )
            {
                // a is below
                Vector2 first = new Vector2(a.Left, a.Top);
                Vector2 second = new Vector2(b.Right, b.Bottom);
                Debug.DrawLine(first, second, Color.red, 0.1f);
                Vector2 v = first - second;
                return (v.magnitude);
            }

            float dist = Mathf.Abs(a.Left - b.Right);
            Vector2 start = new Vector2(a.Left, a.Center.y);
            Debug.DrawLine(start, start + new Vector2(-dist, 0), Color.red, 0.1f);
            return dist;
        }
        if ( a.Right < b.Left )
        {
            // a to left
            if (a.Bottom > b.Top)
            {
                // a is above
                Vector2 first = new Vector2(a.Right, a.Bottom);
                Vector2 second = new Vector2(b.Left, b.Top);
                Debug.DrawLine(first, second, Color.magenta, 0.1f);
                Vector2 v = first - second;
                return (v.magnitude);
            }
            if (a.Top < b.Bottom)
            {
                // a is below
                Vector2 first = new Vector2(a.Right, a.Top);
                Vector2 second = new Vector2(b.Left, b.Bottom);
                Debug.DrawLine(first, second, Color.magenta, 0.1f);
                Vector2 v = first - second;
                return (v.magnitude);
            }

            float dist = Mathf.Abs(a.Right - b.Left);
            Vector2 start = new Vector2(a.Right, a.Center.y);
            Debug.DrawLine(start, start + new Vector2(dist, 0), Color.magenta, 0.1f);
            return dist;
        }

        // In between somewhere
        if ( a.Bottom > b.Top )
        {
            // a is above
            float dist = Mathf.Abs(a.Bottom - b.Top);
            Vector2 start = new Vector2(a.Center.x, a.Bottom);
            Debug.DrawLine(start, start + new Vector2(0, -dist), Color.white, 0.1f);
            return dist;
        }
        if (a.Top < b.Bottom)
        {
            // a is below
            float dist = Mathf.Abs(a.Top - b.Bottom);
            Vector2 start = new Vector2(a.Center.x, a.Top);
            Debug.DrawLine(start, start + new Vector2(0, dist), Color.black, 0.1f);
            return dist;
        }

        a.GetComponent<Renderer>().material.color = Color.white;
        b.GetComponent<Renderer>().material.color = Color.black;

        return 0.0f;
    }

	// Update is called once per frame
	void Update ()
    {
	    if ( a != null && b != null )
        {
            float dist = Distance(a, b);
            Debug.Log(dist);
        }
	}
}
