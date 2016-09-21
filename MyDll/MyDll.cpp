// MyDll.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "MyDll.h"


// This is an example of an exported variable
MYDLL_API int nMyDll=0;

// This is an example of an exported function.
MYDLL_API int fnMyDll(void)
{
	return 42;
}

MYDLL_API int GetStringLength(LPCTSTR sz)
{
    CString str(sz);
    return str.GetLength();
}

MYDLL_API void SayHelloW(wchar_t* wName, OnCallbackDelegateW callback)
{
    CStringW strName(wName);
    CStringW strHello = strName + L" hello";
    int length = (strHello.GetLength() + 1) + sizeof(wchar_t);
    wchar_t* wReturn = (wchar_t*)::CoTaskMemAlloc(length);
    wcsncpy(wReturn, strHello, strHello.GetLength() + 1);
    callback(wReturn);
}

MYDLL_API void SayHello(char* wName, OnCallbackDelegate callback)
{
    CStringA strName(wName);
    CStringA strHello = strName + " hello";
    int length = (strHello.GetLength() + 1) + sizeof(char);
    char* wReturn = (char*)::CoTaskMemAlloc(length);
    strncpy(wReturn, strHello, strHello.GetLength() + 1);
    callback(wReturn);
}

MYDLL_API char* GetSampleString()
{
    char* szHello = "hello황현동";
    char* szReturn = (char*)::CoTaskMemAlloc(strlen(szHello) + 1);
    strncpy(szReturn, szHello, strlen(szHello) + 1);
    return szReturn;
}

MYDLL_API wchar_t* GetSampleWString()
{
    wchar_t* wHello = L"hello황현동";
    int length = (wcslen(wHello) + 1) * sizeof(wchar_t);
    wchar_t* wReturn = (wchar_t*)::CoTaskMemAlloc(length);
    wcsncpy(wReturn, wHello, wcslen(wHello) + 1);
    return wReturn;
}


// This is the constructor of a class that has been exported.
// see MyDll.h for the class definition
CMyDll::CMyDll()
{
	return;
}
