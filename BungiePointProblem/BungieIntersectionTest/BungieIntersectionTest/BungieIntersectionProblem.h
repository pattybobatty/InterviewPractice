//
//  BungieIntersectionProblem.h
//  Intersection Test for Bungie
//
//  Created on 8/6/2019
//  Patrick John Ascolese
//

#ifdef UNITY_DLL_TEST
#define	DLLEXPORT __declspec(dllexport)
#else
#define DLLEXPORT
#endif

struct vector3d
{
	float x;
	float y;
	float z;
};

typedef vector3d point3d;

extern "C"
{
	DLLEXPORT bool intersect_line_segments(point3d position_0, float length_0, point3d position_1, float length_1, vector3d hint_direction, point3d* out_common_end_position);
}