using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSphereOverlap : MonoBehaviour {

    public CapsuleCollider cap;
    public SphereCollider sphere;
	// Use this for initialization
	void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cap && sphere)
        {
            float capLen = cap.height - 2 * cap.radius;
            Vector3 top = cap.transform.position + cap.transform.up * capLen * 0.5f;
            Vector3 bottom = cap.transform.position - cap.transform.up * capLen * 0.5f;
            Debug.DrawLine(top, bottom, Color.red);

            Vector3 closest = GetClosetPointOnLine(top, bottom, sphere.transform.position);

            Debug.DrawLine(sphere.transform.position, closest, Color.green);
            if ( (closest - sphere.transform.position).magnitude < (cap.radius +sphere.radius))
            {
                cap.GetComponent<Renderer>().material.color = Color.green;
                sphere.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                cap.GetComponent<Renderer>().material.color = Color.black;
                sphere.GetComponent<Renderer>().material.color = Color.white;
            }
        }

    }

    Vector3 GetClosetPointOnLine(Vector3 a, Vector3 b, Vector3 t)
    {
        Vector3 BA = (a - b);
        Vector3 AT = t - a;

        Vector3 TonBA = Vector3.Project(AT, BA.normalized);
        float lenBA = BA.magnitude;
        Vector3 tProj = (a + TonBA);
        Debug.DrawLine(t, tProj, Color.magenta, 0.1f);
        Vector3 ATproj = tProj - a;
        Vector3 BTproj = tProj - b;

        if (ATproj.magnitude < lenBA)
        {
            if (BTproj.magnitude < lenBA)
            {
                return tProj;
            }
            return a;
        }
        if (AT.magnitude > BTproj.magnitude)
        {
            return b;
        }
        return a;
    }
}
