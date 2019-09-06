//
//  BungieIntersectionProblem.cpp
//  Intersection Test for Bungie
//
//  Created on 8/6/2019
//  Patrick John Ascolese
//

#include <math.h>
#include "BungieIntersectionProblem.h"

const float epsilon = 0.001f;

vector3d operator+ (vector3d& a, vector3d &b)
{
	vector3d sum;
	sum.x = a.x + b.x;
	sum.y = a.y + b.y;
	sum.z = a.z + b.z;
	return sum;
}

vector3d operator- (vector3d& a, vector3d& b)
{
	vector3d sum;
	sum.x = a.x - b.x;
	sum.y = a.y - b.y;
	sum.z = a.z - b.z;
	return sum;
}

vector3d operator- (vector3d& vector)
{
	vector3d neg;
	neg.x = -vector.x;
	neg.y = -vector.y;
	neg.z = -vector.z;
	return neg;
}

vector3d operator* (float len, vector3d& vector)
{
	vector3d scaledVec;
	scaledVec.x = len * vector.x;
	scaledVec.y = len * vector.y;
	scaledVec.z = len * vector.z;
	return scaledVec;
}

float sq_magnitude(vector3d& vector)
{
	return (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
}

float magnitude(vector3d& vector)
{
#pragma warning( push )
#pragma warning( disable : 4244)
#pragma warning( disable : 26451)
	return sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
#pragma warning( pop ) 
}

vector3d normalize(vector3d& vector)
{
	float mag = magnitude(vector);
	vector3d normalized;
	normalized.x = vector.x / mag;
	normalized.y = vector.y / mag;
	normalized.z = vector.z / mag;
	return normalized;
}

bool approximately_equal(float a, float b)
{
#pragma warning( push )
#pragma warning( disable : 26451)
	return fabs(a - b) <= epsilon;
#pragma warning( pop ) 
}

float cosAngle(float lenA, float lenB, float lenC)
{
	float cosAng = -0.5f * (lenC * lenC - lenA * lenA - lenB * lenB) / (lenA * lenB);
	return cosAng;
}

float dot(vector3d a, vector3d b)
{
	return a.x * b.x + a.y * b.y + a.z * b.z;
}

vector3d project_on_plane(vector3d vector, vector3d normal)
{
	float magSq = dot(normal, normal);
	float dotProd = dot(vector, normal);
	vector3d projOnPlane; 
	projOnPlane.x = vector.x - normal.x * dotProd / magSq;
	projOnPlane.y = vector.y - normal.y * dotProd / magSq,
	projOnPlane.z = vector.z - normal.z * dotProd / magSq;
	return projOnPlane;
}

extern "C"
{
	DLLEXPORT bool intersect_line_segments(point3d position_0, float length_0, point3d position_1, float length_1, vector3d hint_direction, point3d* out_common_end_position)
	{
		if (out_common_end_position == nullptr || length_0 <= 0.0f || length_1 <= 0.0f )
		{
			// invalid parameter
			return false;
		}
		vector3d AB = position_1 - position_0;
		vector3d normAB = normalize(AB);
		float magAB = magnitude(AB);
		
		//
		//  There is exactly 1 intersection point if:
		//   1. The sum of the two lengths is exactly the length of the distance between the spheres
		//       -or-
		//   2. One sphere is inside the other and the larger sphere's radius is equal to the distance between the two plus the radius of the smaller
		//
		if (approximately_equal(magAB, length_0 + length_1) || approximately_equal(length_0, magAB + length_1) || approximately_equal(length_1, magAB + length_0))
		{
			// one touch point
			vector3d dirToTouchPoint = length_0 * normAB;
			*out_common_end_position = position_0 + dirToTouchPoint;
			return true;
		}

		//
		// Hint direction must be a valid vector with length greater than zero
		//
		if (approximately_equal(magnitude(hint_direction), 0.0f))
		{
			//
			// Hint direction is zero length
			//
			return false;
		}

		//
		// There are multiple intersection points if the two spheres are overlapping and if neither sphere is completely contained inside the other
		//
		if (magAB < (length_0 + length_1) &&
			!(length_0 > magAB && length_0 > magAB + length_1) && 
			!(length_1 > magAB && length_1 > magAB + length_0))
		{
			//
			// Many touch points
			//
			float cosAngleAB = cosAngle(length_0, magAB, length_1);
			float lenAonAB = length_0 * cosAngleAB;
			vector3d AC = lenAonAB * normAB;
			
			double magA_sq = ((double)length_0 * (double)length_0);
			float lenO = sqrt(magA_sq - sq_magnitude(AC));

			//
			// project hint vector onto plane orthoganal to AC
			//
			vector3d hintDirOnPlane = project_on_plane(hint_direction, -AC);
			vector3d hintDirOnPlaneNormalized = normalize(hintDirOnPlane);
			point3d centerOfDisc = (position_0 + AC);
			vector3d scaledHintOnPlaneNorm = lenO * hintDirOnPlaneNormalized;
			*out_common_end_position = centerOfDisc + scaledHintOnPlaneNorm;
			return true;
		}

		//
		// No intersections
		//
		return false;
	}
}

