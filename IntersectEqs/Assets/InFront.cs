using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFront : MonoBehaviour {

    public GameObject me;
    public GameObject other;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (me != null && other != null )
        {
            Debug.DrawLine(me.transform.position, me.transform.position + me.transform.forward * 2, Color.green);
            Debug.DrawLine(me.transform.position, me.transform.position + me.transform.right * 2, Color.blue);
            Debug.DrawLine(me.transform.position, me.transform.position - me.transform.right * 2, Color.blue);


            Vector3 otherToMe = other.transform.position - me.transform.position;
            otherToMe.Normalize();

            float dot = Vector3.Dot(me.transform.forward, otherToMe);
            if( dot > 0.0f )
            {
                me.GetComponent<Renderer>().material.color = Color.green;
                other.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                me.GetComponent<Renderer>().material.color = Color.red;
                other.GetComponent<Renderer>().material.color = Color.red;
            }
        }
		
	}
}
