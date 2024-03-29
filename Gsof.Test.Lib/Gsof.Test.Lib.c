﻿// Gsof.Test.Lib.cpp: 定义应用程序的入口点。
//

#include <stdio.h>
#include <string.h>

#define BUILDING_DLL

#if defined _WIN32 || defined __CYGWIN__
#ifdef BUILDING_DLL
#ifdef __GNUC__
#define DLL_PUBLIC __attribute__((dllexport))
#else
#define DLL_PUBLIC __declspec(dllexport) // Note: actually gcc seems to also supports this syntax.
#endif
#else
#ifdef __GNUC__
#define DLL_PUBLIC __attribute__((dllimport))
#else
#define DLL_PUBLIC __declspec(dllimport) // Note: actually gcc seems to also supports this syntax.
#endif
#endif
#define DLL_LOCAL
#else
#if __GNUC__ >= 4
#define DLL_PUBLIC __attribute__((visibility("default")))
#define DLL_LOCAL __attribute__((visibility("hidden")))
#else
#define DLL_PUBLIC
#define DLL_LOCAL
#endif
#endif

// int main()
// {
// 	printf("Hello CMake.");
// 	return 0;
// }

DLL_PUBLIC int test(int input)
{
	return input;
}

DLL_PUBLIC int test_ptr(unsigned char *input, int isize, unsigned char *buffer, int bsize)
{
	int size = isize > bsize ? bsize : isize;
	memcpy(buffer, input, size);
	return 123;
}