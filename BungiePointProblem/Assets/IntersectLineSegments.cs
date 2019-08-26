using UnityEngine;
using System.Runtime.InteropServices;

public class IntersectLineSegments : MonoBehaviour {

    [DllImport("BungieIntersectionTest")]
    private static extern bool intersect_line_segments(Vector3 a, float lenA, Vector3 b, float lenB, Vector3 hintDirection, ref Vector3 out_common_end_point);
    public SphereCollider a;
	public SphereCollider b;
	public SphereCollider hintDirPoint1;

    public float epsilon = 0.001f;
	
	// Update is called once per frame
	void Update () 
	{
		if ( a != null && b != null)
		{
			//a.transform.localScale = 2*a.radius * Vector3.one;
			//b.transform.localScale = 2*b.radius * Vector3.one;
			Vector3 hintDir = hintDirPoint1.transform.position;
			if ( hintDirPoint1 != null )
			{
                Debug.DrawLine(Vector3.zero, hintDirPoint1.transform.position, Color.blue);
			}
			else
			{
				hintDir = Vector3.up;
			}

            Vector3 commonPoint = Vector3.zero;
            
			if (intersect_line_segments(a.transform.position, a.radius, b.transform.position, b.radius, hintDir, ref commonPoint) )
            {
                Debug.DrawLine(commonPoint, commonPoint + 0.25f * Vector3.right, Color.green, 0.0f, false);
                Debug.DrawLine(commonPoint, commonPoint - 0.25f * Vector3.right, Color.green, 0.0f, false);
                Debug.DrawLine(commonPoint, commonPoint + 0.25f * Vector3.up, Color.green, 0.0f, false);
                Debug.DrawLine(commonPoint, commonPoint - 0.25f * Vector3.up, Color.green, 0.0f, false);
                Debug.DrawLine(commonPoint, commonPoint + 0.25f * Vector3.forward, Color.green, 0.0f, false);
                Debug.DrawLine(commonPoint, commonPoint - 0.25f * Vector3.forward, Color.green, 0.0f, false);
            }

		}
	}

    bool approximately_eqaul(float a, float b)
    {
        return Mathf.Abs(a - b) <= epsilon;
    }


    float cosAngle(float lenA, float lenB, float lenC)
    {
        // Law of cosines
        float cosAng = -0.5f * (lenC * lenC - lenA * lenA - lenB * lenB) / (lenA * lenB);
        return cosAng;
    }

    float dot( Vector3 a, Vector3 b )
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    Vector3 project_on_plane(Vector3 vector, Vector3 normal)
    {
        float magSq = dot(normal, normal);
        float dotProd = dot(vector, normal);
        Vector3 projOnPlane = new Vector3(vector.x - normal.x * dotProd / magSq,
                    vector.y - normal.y * dotProd / magSq,
                    vector.z - normal.z * dotProd / magSq);

        return projOnPlane;
    }

    bool intersect_line_segments_unity(Vector3 a, float lenA, Vector3 b, float lenB, Vector3 hintDirection, ref Vector3 out_common_end_position)
	{
		Vector3 AB = b - a;
		if ( approximately_eqaul(AB.magnitude,lenA + lenB) || approximately_eqaul(lenA, AB.magnitude + lenB ) || approximately_eqaul(lenB, AB.magnitude + lenA) )
		{
			// one touch point
			out_common_end_position = a + lenA * AB.normalized;
			return true;
		}
		if (AB.magnitude < (lenA + lenB) && 
			!(lenA > AB.magnitude && lenA > AB.magnitude + lenB ) && !(lenB > AB.magnitude && lenB > AB.magnitude + lenA)) 
		{
			// many touch points
//		    Debug.DrawLine(a, b, Color.green);
			float cosAngleAB = cosAngle(lenA, AB.magnitude, lenB);
			float lenAonAB = lenA * cosAngleAB;
            Vector3 AC = AB.normalized * lenAonAB;
            float angle = Mathf.Acos(cosAngleAB);
            float lenO = Mathf.Tan(angle) * AC.magnitude;

            // project hint vector onto plane orthoganal to AC
            Vector3 hintOnPlane = project_on_plane(hintDirection, -AC);

            out_common_end_position = a + AC + (lenO) * hintOnPlane.normalized;

			Debug.DrawLine(a, a + AC, Color.yellow);
            Debug.DrawLine(a + AC, a + AC + hintDirection, Color.magenta);
			return true;
		}

		return false;
	}

	
}
