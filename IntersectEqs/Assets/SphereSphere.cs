using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSphere : MonoBehaviour {

    public SphereCollider a;
    public SphereCollider b;

    // Use this for initialization
    void Start () {
		
	}

    bool SphereSphereOverlap(SphereCollider A, SphereCollider B)
    {
        float dist = (a.transform.position - b.transform.position).magnitude;
        return (dist < (a.radius + b.radius));
    }
	// Update is called once per frame
	void Update ()
    {
        if (a != null && b != null)
        {
            if (SphereSphereOverlap(a, b))
            {
                a.GetComponent<Renderer>().material.color = Color.green;
                b.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                a.GetComponent<Renderer>().material.color = Color.black;
                b.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
