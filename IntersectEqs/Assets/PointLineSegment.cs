using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLineSegment : MonoBehaviour
{

    public GameObject testPoint;
    public GameObject lineStart;
    public GameObject lineEnd;

    

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (lineStart != null && lineEnd != null)
        {
            Debug.DrawLine(lineStart.transform.position, lineEnd.transform.position, Color.green);

            if (testPoint != null)
            {
                testPoint.GetComponent<Renderer>().material.color = Color.red;
                Vector3 closestPoint = GetClosetPointOnLine(lineStart.transform.position, lineEnd.transform.position, testPoint.transform.position);
                Debug.DrawLine(testPoint.transform.position, closestPoint, Color.red);
            }

            lineStart.GetComponent<Renderer>().material.color = Color.blue;
            lineEnd.GetComponent<Renderer>().material.color = Color.blue;
        }
	}

    Vector3 GetClosetPointOnLine(Vector3 a, Vector3 b, Vector3 t)
    {
        Vector3 BA = (a - b);
        Vector3 AT = t - a;
        
        Vector3 TonBA = Vector3.Project(AT, BA.normalized);
        float lenBA = BA.magnitude;
        Vector3 tProj = (a + TonBA);
        Vector3 ATproj = tProj - a;
        Vector3 BTproj = tProj - b;

        if (ATproj.magnitude < lenBA )
        {
            if (BTproj.magnitude < lenBA)
            {
                return tProj;
            }
            return a;
        }
        return b;
    }
}
