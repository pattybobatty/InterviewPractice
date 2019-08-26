#include "BungieIntersectionProblem.h"
#ifndef UNITY_DLL_TEST
int main()
{
	vector3d test;
	test.x = 2.3743f;
	test.y = 3.18f;
	test.z = 10.7f;

	point3d out;
	intersect_line_segments(test, 3.554f, test, 1.4f, test, &out);
	
	return 0;
}
#endif