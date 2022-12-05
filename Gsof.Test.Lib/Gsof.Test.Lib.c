// Gsof.Test.Lib.cpp: 定义应用程序的入口点。
//

#include <stdio.h>

// int main()
// {
// 	printf("Hello CMake.");
// 	return 0;
// }

__declspec(dllexport) int test(int input)
{
	return input;
}